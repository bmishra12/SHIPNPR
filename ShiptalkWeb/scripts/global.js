// JavaScript Document

    function ddlChange(ddl) {
        if (ddl.selectedIndex == 0) {
            ddl.className = 'dropdown1wm'
        }
        else {
            ddl.className = 'dropdown1';
        }
    }

    function ddl2Change(ddl) {
        if (ddl.selectedIndex == 0) {
            ddl.className = 'dropdown2wm'
        }
        else {
            ddl.className = 'dropdown2';
        }
    }

    function HideDIV(d) {
        document.getElementById(d).style.display = "none";
    }

    function DisplayDIV(d) {
        document.getElementById(d).style.display = "block";
    }

    var activeTab = 1;
    function openTab(tabId) {
        // reset old tab and content
        document.getElementById("tabLink" + activeTab).className = "tabLink";
        // set new tab and content
        document.getElementById("tabLink" + tabId).className = "tabLinkActive";
        activeTab = tabId;
    }


   
