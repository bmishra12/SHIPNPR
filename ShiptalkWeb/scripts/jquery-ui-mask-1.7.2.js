/*
* jQuery UI Mask @VERSION
*
* Copyright (c) 2009 AUTHORS.txt (http://jqueryui.com/about)
* Dual licensed under the MIT (MIT-LICENSE.txt)
* and GPL (GPL-LICENSE.txt) licenses.
*
* Based on the jquery.maskedinput.js plugin by Josh Bush (digitalbush.com)
*
* http://docs.jquery.com/UI/Mask
*
* Depends:
*   ui.core.js
*/
(function($) {

    var pasteEventName = ($.browser.msie ? 'paste' : 'input') + ".mask",
	        iPhone = (window.orientation != undefined),
	        undefined;

    $.widget("ui.mask", {

        _init: function() {

            if (!this.options.mask || !this.options.mask.length) return; //no mask pattern defined. no point in continuing.
            if (!this.options.placeholder || !this.options.placeholder.length) this.options.placeholder = '_'; //in case the user decided to nix a placeholder.

            var widget = this,
                        input = this.element,
                        opts = this.options,
                        mask = opts.mask,
                        defs = $.ui.mask.definitions,
                        tests = [],
                        partialPosition = mask.length,
                        firstNonMaskPos = null,
                        len = mask.length,
                        caret = function(begin, end) { return $.ui.mask.caret(input, begin, end); };

            //if we're applying the mask to an element which is not an input, it won't have a val() method. fake one for our purposes.
            if (!input.is(':input')) input.val = input.html;

            $.each(mask.split(""), function(i, c) {
                if (c == '?') {
                    len--;
                    partialPosition = i;
                }
                else if (defs[c]) {
                    tests.push(new RegExp(defs[c]));
                    if (firstNonMaskPos == null)
                        firstNonMaskPos = tests.length - 1;
                }
                else {
                    tests.push(null);
                }
            });

            var buffer = $.map(mask.split(""), function(c, i) { if (c != '?') return defs[c] ? opts.placeholder : c }),
                        ignore = false,                         //Variable for ignoring control keys
                        focusText = input.val();

            input.data("buffer", buffer).data("tests", tests);

            function seekNext(pos) {
                while (++pos <= len && !tests[pos]);
                return pos;
            };

            function shiftL(pos) {
                while (!tests[pos] && --pos >= 0);
                for (var i = pos; i < len; i++) {
                    if (tests[i]) {
                        buffer[i] = opts.placeholder;
                        var j = seekNext(i);
                        if (j < len && tests[i].test(buffer[j])) {
                            buffer[i] = buffer[j];
                        } else
                            break;
                    }
                }
                writeBuffer();
                caret(Math.max(firstNonMaskPos, pos));
            };

            function shiftR(pos) {
                for (var i = pos, c = opts.placeholder; i < len; i++) {
                    if (tests[i]) {
                        var j = seekNext(i);
                        var t = buffer[i];
                        buffer[i] = c;
                        if (j < len && tests[j].test(t))
                            c = t;
                        else
                            break;
                    }
                }
            };

            function keydownEvent(e) {
                var pos = caret();
                var k = e.keyCode;
                ignore = (k < 16 || (k > 16 && k < 32) || (k > 32 && k < 41));

                //delete selection before proceeding
                if ((pos.begin - pos.end) != 0 && (!ignore || k == 8 || k == 46))
                    clearBuffer(pos.begin, pos.end);

                //backspace, delete, and escape get special treatment
                if (k == 8 || k == 46 || (iPhone && k == 127)) {//backspace/delete
                    shiftL(pos.begin + ((k == 46 || (k == 8 && pos.begin != pos.end)) ? 0 : -1));
                    return false;
                } else if (k == 27) {//escape
                    input.val(focusText);
                    input.caret(0, checkVal());
                    return false;
                }
            };

            function keypressEvent(e) {
                if (ignore) {
                    ignore = false;
                    //Fixes Mac FF bug on backspace
                    return (e.keyCode == 8) ? false : null;
                }
                e = e || window.event;
                var k = e.charCode || e.keyCode || e.which;
                var pos = caret();

                if (e.ctrlKey || e.altKey || e.metaKey) {//Ignore
                    return true;
                } else if ((k >= 32 && k <= 125) || k > 186) {//typeable characters
                    var p = seekNext(pos.begin - 1);
                    if (p < len) {
                        var c = String.fromCharCode(k);
                        if (tests[p].test(c)) {
                            shiftR(p);
                            buffer[p] = c;
                            writeBuffer();
                            var next = seekNext(p);
                            caret(next);
                            if (opts.completed && next == len)
                                opts.completed.call(input);
                        }
                    }
                }
                return false;
            };

            function clearBuffer(start, end) {
                for (var i = start; i < end && i < len; i++) {
                    if (tests[i])
                        buffer[i] = opts.placeholder;
                }
            };

            function writeBuffer() { return input.val(buffer.join('')).val(); };

            function checkVal(allow) {
                //try to place characters where they belong
                var test = input.val();
                var lastMatch = -1;
                for (var i = 0, pos = 0; i < len; i++) {
                    if (tests[i]) {
                        buffer[i] = opts.placeholder;
                        while (pos++ < test.length) {
                            var c = test.charAt(pos - 1);
                            if (tests[i].test(c)) {
                                buffer[i] = c;
                                lastMatch = i;
                                break;
                            }
                        }
                        if (pos > test.length)
                            break;
                    } else if (buffer[i] == test[pos] && i != partialPosition) {
                        pos++;
                        lastMatch = i;
                    }
                }
                if (!allow && lastMatch + 1 < partialPosition) {
                    if (!opts.allowPartials || !widget.value().length) {
                        input.val("");
                        clearBuffer(0, len);
                    }
                    else //if we're allowing partial input/inital values, and the element we're masking isnt an input, then we need to allow the mask to apply.
                        if (!input.is(':input')) writeBuffer();

                } else if (allow || lastMatch + 1 >= partialPosition) {
                    writeBuffer();
                    if (!allow) input.val(input.val().substring(0, lastMatch + 1));
                }
                return (partialPosition ? i : firstNonMaskPos);
            };

            input
	                        .one("unmask", function() {
	                            input
	                                        .unbind(".mask")
	                                        .removeData("buffer")
	                                        .removeData("tests");
	                        })

            if (!input.attr("readonly"))
                input
	                                .bind("focus.mask", function() {
	                                    focusText = input.val();
	                                    var pos = checkVal();
	                                    writeBuffer();
	                                    setTimeout(function() {
	                                        if (pos == mask.length)
	                                            caret(0, pos);
	                                        else
	                                            caret(pos);
	                                    }, 0);
	                                })
	                                .bind("blur.mask", function() {
	                                    checkVal();
	                                    if (input.val() != focusText)
	                                        input.change();
	                                })
	                                .bind('apply.mask', function() { //changing the value of an input without keyboard input requires re-applying the mask.
	                                    focusText = input.val();
	                                    var pos = checkVal();
	                                    writeBuffer();
	                                })
	                                .bind("keydown.mask", keydownEvent)
	                                .bind("keypress.mask", keypressEvent)
	                                .bind(pasteEventName, function() {
	                                    setTimeout(function() { caret(checkVal(true)); }, 0);
	                                });

            checkVal((input.val().length && opts.allowPartials)); //Perform initial check for existing values

        },

        destroy: function() {
            return this.element.trigger("unmask");
        },

        value: function() {
            var input = this.element,
	                        tests = input.data("tests"),
	                        res = $.map(input.data("buffer"), function(c, i) { return tests[i] ? c : null; }).join(''),
	                        r = new RegExp('\\' + this.options.placeholder, 'gi');
            return res.replace(r, '');
        },

        formatted: function() {
            var r = new RegExp('\\' + this.options.placeholder, 'gi'),
	                        res = this.element.val();
            return res.replace(r, '');
        },

        apply: function() {
            this.element.trigger('apply.mask');
        }
    });

    $.extend($.ui.mask, {
        version: "@VERSION",
        getter: "value formatted",
        defaults: {
            mask: '',
            placeholder: '_',
            completed: null,
            allowPartials: $.ui.mask.allowPartials
        },
        definitions: { //Predefined character definitions
            '#': "[\\d]",
            'a': "[A-Za-z]",
            '*': "[A-Za-z0-9]",
            '0': "[0]",
            '1': "[0-1]",
            '2': "[0-2]",
            '3': "[0-3]",
            '4': "[0-4]",
            '5': "[0-5]",
            '6': "[0-6]",
            '7': "[0-7]",
            '8': "[0-8]",
            '9': "[0-9]",
            't': "[ap]|[AP]",
            'm': "m|M"
        },
        caret: function(element, begin, end) {  //Helper Function for Caret positioning
            var input = element[0];
            if (typeof begin == 'number') {
                end = (typeof end == 'number') ? end : begin;
                if (input.setSelectionRange) {
                    input.focus();
                    input.setSelectionRange(begin, end);
                } else if (input.createTextRange) {
                    var range = input.createTextRange();
                    range.collapse(true);
                    range.moveEnd('character', end);
                    range.moveStart('character', begin);
                    range.select();
                }
                return element;
            } else {
                if (input.setSelectionRange) {
                    begin = input.selectionStart;
                    end = input.selectionEnd;
                }
                else if (document.selection && document.selection.createRange) {
                    var range = document.selection.createRange();
                    begin = 0 - range.duplicate().moveStart('character', -100000);
                    end = begin + range.text.length;
                }
                return { begin: begin, end: end };
            }
        }
    });

})(jQuery);