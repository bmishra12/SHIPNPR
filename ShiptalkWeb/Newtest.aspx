﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Newtest.aspx.cs" Inherits="ShiptalkWeb.Newtest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title>jQuery Idle Timeout</title>
<link href="examples.css" rel="stylesheet" type="text/css" />
<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.9/themes/base/jquery-ui.css" type="text/css" rel="stylesheet" />

<!-- we want to force people to click a button, so hide the close link in the toolbar -->
<style type="text/css">a.ui-dialog-titlebar-close { display:none }</style>
</head>
<body>

<div id="bar">
	<h1><a href="http://www.erichynds.com">eric<span>hynds</span></a></h1>
</div>

<!-- dialog window markup -->
<div id="dialog" title="Your session is about to expire!">
	<p>
		<span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 50px 0;"></span>
		You will be logged off in <span id="dialog-countdown" style="font-weight:bold"></span> seconds.
	</p>
	
	<p>Do you want to continue your session?</p>
</div>

<div id="content">
	<h2>jQuery Idle Timeout Demo - jQuery UI Dialog</h2>
	<p>This idle timeout countdown is triggered after 5 seconds.  <strong>Keep your mouse and keyboard still!</strong></p>
	<p>A polling request is sent to the server every two seconds to keep the server side session alive.  I've set this parameter to an <em>extremely</em> low number 
	for demo purposes (timeout is only 5 seconds), but in reality, this should be much higher.</p>
	<p>View source to see markup &amp; CSS.</p>
	
	<ul>
	<li><a href="example-mint.htm">View the original Mint.com example</a></li>
	<li><a href="http://github.com/ehynds/jquery-idle-timeout">Download &amp; follow on GitHub</a></li>
	<li><a href="http://www.erichynds.com">Return to home page</a>
	</ul>
</div>

<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.5/jquery.min.js" type="text/javascript"></script>
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.9/jquery-ui.min.js" type="text/javascript"></script>
<script src="src/jquery.idletimer.js" type="text/javascript"></script>
<script src="src/jquery.idletimeout.js" type="text/javascript"></script>

<script type="text/javascript">
// setup the dialog
$("#dialog").dialog({
	autoOpen: false,
	modal: true,
	width: 400,
	height: 200,
	closeOnEscape: false,
	draggable: false,
	resizable: false,
	buttons: {
		'Yes, Keep Working': function(){
			$(this).dialog('close');
		},
		'No, Logoff': function(){
			// fire whatever the configured onTimeout callback is.
			// using .call(this) keeps the default behavior of "this" being the warning
			// element (the dialog in this case) inside the callback.
			$.idleTimeout.options.onTimeout.call(this);
		}
	}
});

// cache a reference to the countdown element so we don't have to query the DOM for it on each ping.
var $countdown = $("#dialog-countdown");
alert("test");
// start the idle timer plugin
$.idleTimeout('#dialog', 'div.ui-dialog-buttonpane button:first', {
	idleAfter: 5,
	pollingInterval: 2,
	keepAliveURL: 'keepalive.php',
	serverResponseEquals: 'OK',
	onTimeout: function(){
		window.location = "timeout.htm";
	},
	onIdle: function(){
		$(this).dialog("open");
	},
	onCountdown: function(counter){
		$countdown.html(counter); // update the counter
	}
});

</script>

</body>
</html>

