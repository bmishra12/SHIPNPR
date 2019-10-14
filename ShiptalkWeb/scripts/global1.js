//** this is the tab functionality you need to add to the global.js file for the solution.

$(".tabEnter").keypress(function(e) {
    
    
        keyCode = e.which ? e.which : e.keyCode;

        if (keyCode == 13) {
            e.preventDefault();
            $(".tabEnter:eq(" + ($(".tabEnter").index(this) + 1) + ")").focus();
        }
    });

