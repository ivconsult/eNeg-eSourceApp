﻿<%@ Page Language="c#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="citPOINT.eSourceApp.Web.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>eSource App</title>
    <style type="text/css">
        html, body
        {
            height: 100%;
            overflow: hidden;
        }
        body
        {
            padding: 0;
            margin: 0;
        }
        #silverlightControlHost
        {
            height: 100%;
            text-align: center;
        }
    </style>
    <script type="text/javascript" src="Silverlight.js"></script>
    <script type="text/javascript">

        function OnLoading() {

            //            window.innerWidth = screen.width;
            //            window.innerHeight = screen.height;
            //            window.screenX = 0;
            //            window.screenY = 0;
            //            alwaysLowered = false;
            try {


//                top.window.moveTo(0, 0);
//                if (document.all) {
//                    top.window.resizeTo(screen.availWidth, screen.availHeight);
//                }
//                else if (document.layers || document.getElementById) {
//                    if (top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth) {
//                        top.window.outerHeight = top.screen.availHeight;
//                        top.window.outerWidth = top.screen.availWidth;

//                    }
//                }
//                window.focus();
            }
            catch (e) {

            }
        }

        function onSilverlightError(sender, args) {
            var appSource = "";
            if (sender != null && sender != 0) {
                appSource = sender.getHost().Source;
            }

            var errorType = args.ErrorType;
            var iErrorCode = args.ErrorCode;

            if (errorType == "ImageError" || errorType == "MediaError") {
                return;
            }

            var errMsg = "Unhandled Error in Silverlight Application " + appSource + "\n";

            errMsg += "Code: " + iErrorCode + "    \n";
            errMsg += "Category: " + errorType + "       \n";
            errMsg += "Message: " + args.ErrorMessage + "     \n";

            if (errorType == "ParserError") {
                errMsg += "File: " + args.xamlFile + "     \n";
                errMsg += "Line: " + args.lineNumber + "     \n";
                errMsg += "Position: " + args.charPosition + "     \n";
            }
            else if (errorType == "RuntimeError") {
                if (args.lineNumber != 0) {
                    errMsg += "Line: " + args.lineNumber + "     \n";
                    errMsg += "Position: " + args.charPosition + "     \n";
                }
                errMsg += "MethodName: " + args.methodName + "     \n";
            }

            throw new Error(errMsg);
        }
    </script>
</head>
<body onload="OnLoading();">
    <form id="form1" runat="server" style="height: 100%">
    <div id="silverlightControlHost">
        <object data="data:application/x-silverlight-2," type="application/x-silverlight-2"
            width="100%" height="100%">
            <param name="source" value="ClientBin/citPOINT.eSourceApp.Client.xap" />
            <param name="onError" value="onSilverlightError" />
            <param name="background" value="white" />
            <param name="minRuntimeVersion" value="4.0.50401.0" />
            <param name="autoUpgrade" value="false" />
            <param name="windowless" value="true"/>
            <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=4.0.50401.0" style="text-decoration: none">
                <img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="Get Microsoft Silverlight"
                    style="border-style: none" />
            </a>
        </object>
        <iframe id="_sl_historyFrame" style="visibility: hidden; height: 0px; width: 0px;
            border: 0px"></iframe>
    </div>
    </form>
</body>
</html>
