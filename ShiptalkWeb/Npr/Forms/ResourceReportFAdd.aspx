<%@ Page Language="C#" MasterPageFile="~/ShiptalkWebWide.Master" AutoEventWireup="true" EnableViewState="true"
    CodeBehind="ResourceReportFAdd.aspx.cs" Inherits="ShiptalkWeb.Npr.ResourceReportF.ResourceReportFAdd" %>
<%@ Import Namespace="ShiptalkWeb" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" TagPrefix="pp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body1" runat="server">



    <script type="text/javascript" src="../../../scripts/jquery-ui-mask-1.7.2.js"></script>

    <script type="text/javascript" src="../../../scripts/jquery.charcounter.js"></script>
    <script type="text/javascript" src="../../../scripts/global1.js"></script>
    <script type="text/javascript">

        
      function isNumberKey(evt)
      {
         var charCode = (evt.which) ? evt.which : event.keyCode
         if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
         return true;
     }

     $(document).ready(function() {


         $("[id$='_txtStateVolCoun']").blur(function() {

             var Val1 = $("[id$='_txtStateVolCoun']");
             var Val2 = $("[id$='_txtOtherVolCoun']");
             var Total = $("[id$='_txtTotalVolCoun']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();

         });

         $(".tabEnter").keypress(function(e) {
             keyCode = e.which ? e.which : e.keyCode;

             if (keyCode == 13) {
                 e.preventDefault();
                 $(".tabEnter:eq(" + ($(".tabEnter").index(this) + 1) + ")").focus();
             }
         });


         $("[id$='_txtOtherVolCoun']").blur(function() {

             var Val1 = $("[id$='_txtStateVolCoun']");
             var Val2 = $("[id$='_txtOtherVolCoun']");
             var Total = $("[id$='_txtTotalVolCoun']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();
         });

         $("[id$='_txtTotalVolCoun']").blur(function() {

             var Val1 = $("[id$='_txtStateVolCoun']");
             var Val2 = $("[id$='_txtOtherVolCoun']");
             var Total = $("[id$='_txtTotalVolCoun']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();
         });



         //*****************************************************  

         $("[id$='_txtStateShipPdCoun']").blur(function() {

             var Val1 = $("[id$='_txtStateShipPdCoun']");
             var Val2 = $("[id$='_txtOtherShipPdCoun']");
             var Total = $("[id$='_txtTotalShipPdCoun']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();
         });


         $("[id$='_txtOtherShipPdCoun']").blur(function() {

             var Val1 = $("[id$='_txtStateShipPdCoun']");
             var Val2 = $("[id$='_txtOtherShipPdCoun']");
             var Total = $("[id$='_txtTotalShipPdCoun']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();
         });


         $("[id$='_txtTotalShipPdCoun']").blur(function() {

             var Val1 = $("[id$='_txtStateShipPdCoun']");
             var Val2 = $("[id$='_txtOtherShipPdCoun']");
             var Total = $("[id$='_txtTotalShipPdCoun']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();
         });

         //*******************************************************
         $("[id$='_txtStateInKindPdCoun']").blur(function() {

             var Val1 = $("[id$='_txtStateInKindPdCoun']");
             var Val2 = $("[id$='_txtOtherInKindPdCoun']");
             var Total = $("[id$='_txtTotalInKindPdCoun']");
             Calc(Val1, Val2, Total);
         });

         $("[id$='_txtOtherInKindPdCoun']").blur(function() {

             var Val1 = $("[id$='_txtStateInKindPdCoun']");
             var Val2 = $("[id$='_txtOtherInKindPdCoun']");
             var Total = $("[id$='_txtTotalInKindPdCoun']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();
         });


         $("[id$='_txtTotalInKindPdCoun']").blur(function() {

             var Val1 = $("[id$='_txtStateInKindPdCoun']");
             var Val2 = $("[id$='_txtOtherInKindPdCoun']");
             var Total = $("[id$='_txtTotalInKindPdCoun']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();
         });

         //*********************************************************
         $("[id$='_txtStateVolCoun']").blur(function() {

             var Val1 = $("[id$='_txtStateVolCoun']");
             var Val2 = $("[id$='_txtOtherVolCoun']");
             var Total = $("[id$='_txtTotalVolCoun']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();
         });


         $("[id$='_txtStateVolCoun']").blur(function() {

             var Val1 = $("[id$='_txtStateVolCoun']");
             var Val2 = $("[id$='_txtOtherVolCoun']");
             var Total = $("[id$='_txtTotalVolCoun']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();
         });

         $("[id$='_txtTotalVolCoun']").blur(function() {

             var Val1 = $("[id$='_txtStateVolCoun']");
             var Val2 = $("[id$='_txtOtherVolCoun']");
             var Total = $("[id$='_txtTotalVolCoun']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();
         });

         //*********************************************************

         $("[id$='_txtStateShipPdCoun']").blur(function() {

             var Val1 = $("[id$='_txtStateShipPdCoun']");
             var Val2 = $("[id$='_txtOtherShipPdCoun']");
             var Total = $("[id$='_txtTotalShipPdCoun']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();
         });

         $("[id$='_txtOtherShipPdCoun']").blur(function() {

             var Val1 = $("[id$='_txtStateShipPdCoun']");
             var Val2 = $("[id$='_txtOtherShipPdCoun']");
             var Total = $("[id$='_txtTotalShipPdCoun']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();
         });

         $("[id$='_txtTotalShipPdCoun']").blur(function() {

             var Val1 = $("[id$='_txtStateShipPdCoun']");
             var Val2 = $("[id$='_txtOtherShipPdCoun']");
             var Total = $("[id$='_txtTotalShipPdCoun']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();
         });

         //************************************************************
         $("[id$='_txtStateInKindPd']").blur(function() {

             var Val1 = $("[id$='_txtStateInKindPd']");
             var Val2 = $("[id$='_txtOtherInKindPd']");
             var Total = $("[id$='_txtTotalInKindPd']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();
         });

         $("[id$='_txtOtherInKindPd']").blur(function() {

             var Val1 = $("[id$='_txtStateInKindPd']");
             var Val2 = $("[id$='_txtOtherInKindPd']");
             var Total = $("[id$='_txtTotalInKindPd']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();
         });

         $("[id$='_txtTotalInKindPd']").blur(function() {

             var Val1 = $("[id$='_txtStateInKindPd']");
             var Val2 = $("[id$='_txtOtherInKindPd']");
             var Total = $("[id$='_txtTotalInKindPd']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();
         });

         //************************************************************

         $("[id$='_txtStateInKindPd']").blur(function() {

             var Val1 = $("[id$='_txtStateInKindPd']");
             var Val2 = $("[id$='_txtOtherInKindPd']");
             var Total = $("[id$='_txtTotalInKindPd']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();
         });

         $("[id$='_txtOtherInKindPd']").blur(function() {

             var Val1 = $("[id$='_txtStateInKindPd']");
             var Val2 = $("[id$='_txtOtherInKindPd']");
             var Total = $("[id$='_txtTotalInKindPd']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();
         });

         $("[id$='_txtTotalInKindPdHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateInKindPdHrs']");
             var Val2 = $("[id$='_txtOtherInKindPdHrs']");
             var Total = $("[id$='_txtTotalInKindPdHrs']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalABC();
         });
         //*****************ABC****************************

         $("[id$='_txtStateVolCoun']").blur(function() {

             var Val1 = $("[id$='_txtStateVolCoun']");
             var Val2 = $("[id$='_txtStateShipPdCoun']");
             var Var3 = $("[id$='_txtStateInKindPdCoun']");
             var Total = $("[id$='_txtStateTotalCoun']");
             CalcVertical(Val1, Val2, Var3, Total);
             CalcCompleteTotalABC();
         });



         $("[id$='_txtStateShipPdCoun']").blur(function() {

             var Val1 = $("[id$='_txtStateVolCoun']");
             var Val2 = $("[id$='_txtStateShipPdCoun']");
             var Var3 = $("[id$='_txtStateInKindPdCoun']");
             var Total = $("[id$='_txtStateTotalCoun']");
             CalcVertical(Val1, Val2, Var3, Total);
             CalcCompleteTotalABC();
         });


         $("[id$='_txtStateInKindPdCoun']").blur(function() {

             var Val1 = $("[id$='_txtStateVolCoun']");
             var Val2 = $("[id$='_txtStateShipPdCoun']");
             var Var3 = $("[id$='_txtStateInKindPdCoun']");
             var Total = $("[id$='_txtStateTotalCoun']");
             CalcVertical(Val1, Val2, Var3, Total);
             CalcCompleteTotalABC();
         });


         $("[id$='_txtOtherVolCoun']").blur(function() {

             var Val1 = $("[id$='_txtOtherVolCoun']");
             var Val2 = $("[id$='_txtOtherShipPdCoun']");
             var Var3 = $("[id$='_txtOtherInKindPdCoun']");
             var Total = $("[id$='_txtOtherTotalCoun']");
             CalcVertical(Val1, Val2, Var3, Total);
             CalcCompleteTotalABC();
         });


         $("[id$='_txtOtherShipPdCoun']").blur(function() {

             var Val1 = $("[id$='_txtOtherVolCoun']");
             var Val2 = $("[id$='_txtOtherShipPdCoun']");
             var Var3 = $("[id$='_txtOtherInKindPdCoun']");
             var Total = $("[id$='_txtOtherTotalCoun']");
             CalcVertical(Val1, Val2, Var3, Total);
             CalcCompleteTotalABC();
         });

         $("[id$='_txtOtherInKindPdCoun']").blur(function() {

             var Val1 = $("[id$='_txtOtherVolCoun']");
             var Val2 = $("[id$='_txtOtherShipPdCoun']");
             var Var3 = $("[id$='_txtOtherInKindPdCoun']");
             var Total = $("[id$='_txtOtherTotalCoun']");
             CalcVertical(Val1, Val2, Var3, Total);
             CalcCompleteTotalABC();


         });


         ////*****************DEF****************************



         $("[id$='_txtStateVolCounHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateVolCounHrs']");
             var Val2 = $("[id$='_txtOtherVolCounHrs']");
             var Total = $("[id$='_txtTotalVolCounHrs']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalDEF();
         });


         $("[id$='_txtOtherVolCounHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateVolCounHrs']");
             var Val2 = $("[id$='_txtOtherVolCounHrs']");
             var Total = $("[id$='_txtTotalVolCounHrs']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalDEF();

         });

         $("[id$='_txtTotalVolCounHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateVolCounHrs']");
             var Val2 = $("[id$='_txtOtherVolCounHrs']");
             var Total = $("[id$='_txtTotalVolCounHrs']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalDEF();

         });


         //**********E***************
         $("[id$='_txtStateShipPdCounHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateShipPdCounHrs']");
             var Val2 = $("[id$='_txtOtherShipPdCounHrs']");
             var Total = $("[id$='_txtTotalShipPdCounHrs']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalDEF();
         });

         $("[id$='_txtOtherShipPdCounHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateShipPdCounHrs']");
             var Val2 = $("[id$='_txtOtherShipPdCounHrs']");
             var Total = $("[id$='_txtTotalShipPdCounHrs']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalDEF();
         });

         $("[id$='_txtTotalShipPdCounHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateShipPdCounHrs']");
             var Val2 = $("[id$='_txtOtherShipPdCounHrs']");
             var Total = $("[id$='_txtTotalShipPdCounHrs']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalDEF();
         });

         //*******************F**********************


         $("[id$='_txtStateInKindPdHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateInKindPdHrs']");
             var Val2 = $("[id$='_txtOtherInKindPdHrs']");
             var Total = $("[id$='_txtTotalInKindPdHrs']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalDEF();
         });

         $("[id$='_txtOtherInKindPdHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateInKindPdHrs']");
             var Val2 = $("[id$='_txtOtherInKindPdHrs']");
             var Total = $("[id$='_txtTotalInKindPdHrs']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalDEF();
         });

         $("[id$='_txtTotalInKindPdHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateInKindPdHrs']");
             var Val2 = $("[id$='_txtOtherInKindPdHrs']");
             var Total = $("[id$='_txtTotalInKindPdHrs']");
             Calc(Val1, Val2, Total);
             CalcCompleteTotalDEF();
         });





         //**********************************TOTAL DEF*****************************

         $("[id$='_txtStateVolCounHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateVolCounHrs']");
             var Val2 = $("[id$='_txtStateShipPdCounHrs']");
             var Var3 = $("[id$='_txtStateInKindPdHrs']");
             var Total = $("[id$='_txtStateTotalCounHrs']");
             CalcVertical(Val1, Val2, Var3, Total);
             CalcCompleteTotalDEF();

         });

         $("[id$='_txtStateShipPdCounHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateVolCounHrs']");
             var Val2 = $("[id$='_txtStateShipPdCounHrs']");
             var Var3 = $("[id$='_txtStateInKindPdHrs']");
             var Total = $("[id$='_txtStateTotalCounHrs']");
             CalcVertical(Val1, Val2, Var3, Total);
             CalcCompleteTotalDEF();

         });

         $("[id$='_txtStateInKindPdHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateVolCounHrs']");
             var Val2 = $("[id$='_txtStateShipPdCounHrs']");
             var Var3 = $("[id$='_txtStateInKindPdHrs']");
             var Total = $("[id$='_txtStateTotalCounHrs']");
             CalcVertical(Val1, Val2, Var3, Total);
             CalcCompleteTotalDEF();

         });


         $("[id$='_txtStateTotalCounHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateTotalCounHrs']");
             var Val2 = $("[id$='_txtOtherTotalCounHrs']");
             var Var3 = $("[id$='_txtTotalInKindPdHrs']");
             var Total = $("[id$='_txtTotalTotalCounHrs']");
             CalcVertical(Val1, Val2, Var3, Total);
             CalcCompleteTotalDEF();

         });

         $("[id$='_txtOtherTotalCounHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateTotalCounHrs']");
             var Val2 = $("[id$='_txtOtherTotalCounHrs']");
             var Var3 = $("[id$='_txtTotalInKindPdHrs']");
             var Total = $("[id$='_txtTotalTotalCounHrs']");
             CalcVertical(Val1, Val2, Var3, Total);
             CalcCompleteTotalDEF();

         });


         $("[id$='_txtTotalTotalCounHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateTotalCounHrs']");
             var Val2 = $("[id$='_txtOtherTotalCounHrs']");
             var Var3 = $("[id$='_txtTotalInKindPdHrs']");
             var Total = $("[id$='_txtTotalTotalCounHrs']");
             CalcVertical(Val1, Val2, Var3, Total);
             CalcCompleteTotalDEF();

         });


         $("[id$='_txtOtherVolCounHrs']").blur(function() {

             var Val1 = $("[id$='_txtOtherVolCounHrs']");
             var Val2 = $("[id$='_txtOtherShipPdCounHrs']");
             var Var3 = $("[id$='_txtOtherInKindPdHrs']");
             var Total = $("[id$='_txtOtherTotalCounHrs']");
             CalcVertical(Val1, Val2, Var3, Total);
             CalcCompleteTotalDEF();

         });

         $("[id$='_txtOtherShipPdCounHrs']").blur(function() {

             var Val1 = $("[id$='_txtOtherVolCounHrs']");
             var Val2 = $("[id$='_txtOtherShipPdCounHrs']");
             var Var3 = $("[id$='_txtOtherInKindPdHrs']");
             var Total = $("[id$='_txtOtherTotalCounHrs']");
             CalcVertical(Val1, Val2, Var3, Total);
             CalcCompleteTotalDEF();

         });


         $("[id$='_txtOtherInKindPdHrs']").blur(function() {

             var Val1 = $("[id$='_txtOtherVolCounHrs']");
             var Val2 = $("[id$='_txtOtherShipPdCounHrs']");
             var Var3 = $("[id$='_txtOtherInKindPdHrs']");
             var Total = $("[id$='_txtOtherTotalCounHrs']");
             CalcVertical(Val1, Val2, Var3, Total);
             CalcCompleteTotalDEF();

         });


         //***************************Section 3********************

         $("[id$='_txtStateVolOtherStaff']").blur(function() {

             var Val1 = $("[id$='_txtStateVolOtherStaff']");
             var Val2 = $("[id$='_txtOtherVolOtherStaff']");
             var Total = $("[id$='_txtTotalVolOtherStaff']");
             Calc(Val1, Val2, Total);
             CalcSect3ABC();

         })

         $("[id$='_txtOtherVolOtherStaff']").blur(function() {

             var Val1 = $("[id$='_txtStateVolOtherStaff']");
             var Val2 = $("[id$='_txtOtherVolOtherStaff']");
             var Total = $("[id$='_txtTotalVolOtherStaff']");
             Calc(Val1, Val2, Total);
             CalcSect3ABC();

         })

         $("[id$='_txtTotalVolOtherStaff']").blur(function() {

             var Val1 = $("[id$='_txtStateVolOtherStaff']");
             var Val2 = $("[id$='_txtOtherVolOtherStaff']");
             var Total = $("[id$='_txtTotalVolOtherStaff']");
             Calc(Val1, Val2, Total);
             CalcSect3ABC();

         })
         //**************sect 3b**************
         $("[id$='_txtStateShipPdOtherStaff']").blur(function() {

             var Val1 = $("[id$='_txtStateShipPdOtherStaff']");
             var Val2 = $("[id$='_txtOtherShipPdOtherStaff']");
             var Total = $("[id$='_txtTotalShipPdOtherStaff']");
             Calc(Val1, Val2, Total);
             CalcSect3ABC();

         })

         $("[id$='_txtOtherShipPdOtherStaff']").blur(function() {

             var Val1 = $("[id$='_txtStateShipPdOtherStaff']");
             var Val2 = $("[id$='_txtOtherShipPdOtherStaff']");
             var Total = $("[id$='_txtTotalShipPdOtherStaff']");
             Calc(Val1, Val2, Total);
             CalcSect3ABC();

         })

         $("[id$='_txtTotalInKindPdOtherStaff']").blur(function() {

             var Val1 = $("[id$='_txtStateShipPdOtherStaff']");
             var Val2 = $("[id$='_txtOtherShipPdOtherStaff']");
             var Total = $("[id$='_txtTotalShipPdOtherStaff']");
             Calc(Val1, Val2, Total);
             CalcSect3ABC();

         })

         //*****************sect 3c**********************
         $("[id$='_txtStateInKindPdOtherStaff']").blur(function() {

             var Val1 = $("[id$='_txtStateInKindPdOtherStaff']");
             var Val2 = $("[id$='_txtOtherInKindPdOtherStaff']");
             var Total = $("[id$='_txtTotalInKindPdOtherStaff']");
             Calc(Val1, Val2, Total);
             CalcSect3ABC();

         })

         $("[id$='_txtOtherInKindPdOtherStaff']").blur(function() {

             var Val1 = $("[id$='_txtStateInKindPdOtherStaff']");
             var Val2 = $("[id$='_txtOtherInKindPdOtherStaff']");
             var Total = $("[id$='_txtTotalInKindPdOtherStaff']");
             Calc(Val1, Val2, Total);
             CalcSect3ABC();

         })

         $("[id$='_txtTotalInKindPdOtherStaff']").blur(function() {

             var Val1 = $("[id$='_txtStateInKindPdOtherStaff']");
             var Val2 = $("[id$='_txtOtherInKindPdOtherStaff']");
             var Total = $("[id$='_txtTotalInKindPdOtherStaff']");
             Calc(Val1, Val2, Total);
             CalcSect3ABC();

         })



         //3c vertical
         $("[id$='_txtStateVolOtherStaff']").blur(function() {

             var Val1 = $("[id$='_txtStateVolOtherStaff']");
             var Val2 = $("[id$='_txtStateShipPdOtherStaff']");
             var Val3 = $("[id$='_txtStateInKindPdOtherStaff']");
             var Total = $("[id$='_txtStateTotalOtherStaff']");
             CalcVertical(Val1, Val2, Val3, Total);
             CalcSect3ABC();

         })

         $("[id$='_txtStateShipPdOtherStaff']").blur(function() {

             var Val1 = $("[id$='_txtStateVolOtherStaff']");
             var Val2 = $("[id$='_txtStateShipPdOtherStaff']");
             var Val3 = $("[id$='_txtStateInKindPdOtherStaff']");
             var Total = $("[id$='_txtStateTotalOtherStaff']");
             CalcVertical(Val1, Val2, Val3, Total);
             CalcSect3ABC();

         })

         $("[id$='_txtStateInKindPdOtherStaff']").blur(function() {

             var Val1 = $("[id$='_txtStateVolOtherStaff']");
             var Val2 = $("[id$='_txtStateShipPdOtherStaff']");
             var Val3 = $("[id$='_txtStateInKindPdOtherStaff']");
             var Total = $("[id$='_txtStateTotalOtherStaff']");
             CalcVertical(Val1, Val2, Val3, Total);
             CalcSect3ABC();

         })

         $("[id$='_txtOtherVolOtherStaff']").blur(function() {

             var Val1 = $("[id$='_txtOtherVolOtherStaff']");
             var Val2 = $("[id$='_txtOtherShipPdOtherStaff']");
             var Val3 = $("[id$='_txtOtherInKindPdOtherStaff']");
             var Total = $("[id$='_txtOtherTotalOtherStaff']");
             CalcVertical(Val1, Val2, Val3, Total);
             CalcSect3ABC();

         })
         $("[id$='_txtOtherShipPdOtherStaff']").blur(function() {

             var Val1 = $("[id$='_txtOtherVolOtherStaff']");
             var Val2 = $("[id$='_txtOtherShipPdOtherStaff']");
             var Val3 = $("[id$='_txtOtherInKindPdOtherStaff']");
             var Total = $("[id$='_txtOtherTotalOtherStaff']");
             CalcVertical(Val1, Val2, Val3, Total);
             CalcSect3ABC();

         })

         $("[id$='_txtOtherInKindPdOtherStaff']").blur(function() {

             var Val1 = $("[id$='_txtOtherVolOtherStaff']");
             var Val2 = $("[id$='_txtOtherShipPdOtherStaff']");
             var Val3 = $("[id$='_txtOtherInKindPdOtherStaff']");
             var Total = $("[id$='_txtOtherTotalOtherStaff']");
             CalcVertical(Val1, Val2, Val3, Total);
             CalcSect3ABC();

         })

         //************************Section 2************************
         //*********************Section 2******************************
         $("[id$='_txtTotalUnPdCoord']").blur(function() {

             var Val1 = $("[id$='_txtTotalUnPdCoord']");
             var Val2 = $("[id$='_txtTotalShipPdCoord']");
             var Val3 = $("[id$='_txtTotalInKindPdCoord']");
             var Total = $("[id$='_txtTotalCoord']");
             CalcVertical(Val1, Val2, Val3, Total);

         })

         $("[id$='_txtTotalShipPdCoord']").blur(function() {

             var Val1 = $("[id$='_txtTotalUnPdCoord']");
             var Val2 = $("[id$='_txtTotalShipPdCoord']");
             var Val3 = $("[id$='_txtTotalInKindPdCoord']");
             var Total = $("[id$='_txtTotalCoord']");
             CalcVertical(Val1, Val2, Val3, Total);

         })

         $("[id$='_txtTotalInKindPdCoord']").blur(function() {

             var Val1 = $("[id$='_txtTotalUnPdCoord']");
             var Val2 = $("[id$='_txtTotalShipPdCoord']");
             var Val3 = $("[id$='_txtTotalInKindPdCoord']");
             var Total = $("[id$='_txtTotalCoord']");
             CalcVertical(Val1, Val2, Val3, Total);

         })

         $("[id$='_txtTotalCoord']").blur(function() {

             var Val1 = $("[id$='_txtTotalUnPdCoord']");
             var Val2 = $("[id$='_txtTotalShipPdCoord']");
             var Val3 = $("[id$='_txtTotalInKindPdCoord']");
             var Total = $("[id$='_txtTotalCoord']");
             CalcVertical(Val1, Val2, Val3, Total);

         })






         $("[id$='_txtTotalUnPdCoordHrs']").blur(function() {
             var Val1 = $("[id$='_txtTotalUnPdCoordHrs']");
             var Val2 = $("[id$='_txtTotalShipPdCoordHrs']");
             var Val3 = $("[id$='_txtTotalInKindPdCoordHrs']");
             var Total = $("[id$='_txtTotalCoordHrs']");
             CalcVertical(Val1, Val2, Val3, Total);
         })

         $("[id$='_txtTotalShipPdCoordHrs']").blur(function() {
             var Val1 = $("[id$='_txtTotalUnPdCoordHrs']");
             var Val2 = $("[id$='_txtTotalShipPdCoordHrs']");
             var Val3 = $("[id$='_txtTotalInKindPdCoordHrs']");
             var Total = $("[id$='_txtTotalCoordHrs']");
             CalcVertical(Val1, Val2, Val3, Total);
         })


         $("[id$='_txtTotalInKindPdCoordHrs']").blur(function() {
             var Val1 = $("[id$='_txtTotalUnPdCoordHrs']");
             var Val2 = $("[id$='_txtTotalShipPdCoordHrs']");
             var Val3 = $("[id$='_txtTotalInKindPdCoordHrs']");
             var Total = $("[id$='_txtTotalCoordHrs']");
             CalcVertical(Val1, Val2, Val3, Total);
         })


         $("[id$='_txtTotalCoordHrs']").blur(function() {
             var Val1 = $("[id$='_txtTotalUnPdCoordHrs']");
             var Val2 = $("[id$='_txtTotalShipPdCoordHrs']");
             var Val3 = $("[id$='_txtTotalInKindPdCoordHrs']");
             var Total = $("[id$='_txtTotalCoordHrs']");
             CalcVertical(Val1, Val2, Val3, Total);
         })






         //********************Sect 3 DEF*********************************

         $("[id$='_txtStateVolOtherStaffHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateVolOtherStaffHrs']");
             var Val2 = $("[id$='_txtOtherVolOtherStaffHrs']");
             var Total = $("[id$='_txtTotalVolOtherStaffHrs']");
             Calc(Val1, Val2, Total);
             CalcSect3DEF();

         })

         $("[id$='_txtOtherVolOtherStaffHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateVolOtherStaffHrs']");
             var Val2 = $("[id$='_txtOtherVolOtherStaffHrs']");
             var Total = $("[id$='_txtTotalVolOtherStaffHrs']");
             Calc(Val1, Val2, Total);
             CalcSect3DEF();

         })

         $("[id$='_txtTotalVolOtherStaffHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateVolOtherStaffHrs']");
             var Val2 = $("[id$='_txtOtherVolOtherStaffHrs']");
             var Total = $("[id$='_txtTotalVolOtherStaffHrs']");
             Calc(Val1, Val2, Total);
             CalcSect3DEF();

         })

         $("[id$='_txtStateShipPdOtherStaffHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateShipPdOtherStaffHrs']");
             var Val2 = $("[id$='_txtOtherShipPdOtherStaffHrs']");
             var Total = $("[id$='_txtTotalShipPdOtherStaffHrs']");

             Calc(Val1, Val2, Total);
             CalcSect3DEF();

         })


         $("[id$='_txtOtherShipPdOtherStaffHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateShipPdOtherStaffHrs']");
             var Val2 = $("[id$='_txtOtherShipPdOtherStaffHrs']");
             var Total = $("[id$='_txtTotalShipPdOtherStaffHrs']");
             Calc(Val1, Val2, Total);
             CalcSect3DEF();

         })

         $("[id$='_txtTotalShipPdOtherStaffHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateShipPdOtherStaffHrs']");
             var Val2 = $("[id$='_txtOtherShipPdOtherStaffHrs']");
             var Total = $("[id$='_txtTotalShipPdOtherStaffHrs']");
             Calc(Val1, Val2, Total);
             CalcSect3DEF();

         })

         $("[id$='_txtStateInKindPdOtherStaffHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateInKindPdOtherStaffHrs']");
             var Val2 = $("[id$='_txtOtherInKindPdOtherStaffHrs']");
             var Total = $("[id$='_txtTotalInKindPdOtherStaffHrs']");
             Calc(Val1, Val2, Total);
             CalcSect3DEF();

         })

         $("[id$='_txtOtherInKindPdOtherStaffHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateInKindPdOtherStaffHrs']");
             var Val2 = $("[id$='_txtOtherInKindPdOtherStaffHrs']");
             var Total = $("[id$='_txtTotalInKindPdOtherStaffHrs']");
             Calc(Val1, Val2, Total);
             CalcSect3DEF();

         })

         $("[id$='_txtTotalInKindPdOtherStaffHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateInKindPdOtherStaffHrs']");
             var Val2 = $("[id$='_txtOtherInKindPdOtherStaffHrs']");
             var Total = $("[id$='_txtTotalInKindPdOtherStaffHrs']");
             Calc(Val1, Val2, Total);


         })


         //sect 3 def vert
         $("[id$='_txtStateVolOtherStaffHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateVolOtherStaffHrs']");
             var Val2 = $("[id$='_txtStateShipPdOtherStaffHrs']");
             var Val3 = $("[id$='_txtStateInKindPdOtherStaffHrs']");
             var Total = $("[id$='_txtStateTotalOtherStaffHrs']");
             CalcVertical(Val1, Val2, Val3, Total);
             CalcSect3DEF();

         })

         $("[id$='_txtStateShipPdOtherStaffHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateVolOtherStaffHrs']");
             var Val2 = $("[id$='_txtStateShipPdOtherStaffHrs']");
             var Val3 = $("[id$='_txtStateInKindPdOtherStaffHrs']");
             var Total = $("[id$='_txtStateTotalOtherStaffHrs']");
             CalcVertical(Val1, Val2, Val3, Total);
             CalcSect3DEF();

         })

         $("[id$='_txtStateInKindPdOtherStaffHrs']").blur(function() {

             var Val1 = $("[id$='_txtStateVolOtherStaffHrs']");
             var Val2 = $("[id$='_txtStateShipPdOtherStaffHrs']");
             var Val3 = $("[id$='_txtStateInKindPdOtherStaffHrs']");
             var Total = $("[id$='_txtStateTotalOtherStaffHrs']");
             CalcVertical(Val1, Val2, Val3, Total);
             CalcSect3DEF();

         })

         //Sub totals
         $("[id$='_txtOtherVolOtherStaffHrs']").blur(function() {

             var Val1 = $("[id$='_txtOtherVolOtherStaffHrs']");
             var Val2 = $("[id$='_txtOtherShipPdOtherStaffHrs']");
             var Val3 = $("[id$='_txtOtherInKindPdOtherStaffHrs']");
             var Total = $("[id$='_txtOtherTotalOtherStaffHrs']");
             CalcVertical(Val1, Val2, Val3, Total);
             CalcSect3DEF();

         })


         $("[id$='_txtOtherShipPdOtherStaffHrs']").blur(function() {

             var Val1 = $("[id$='_txtOtherVolOtherStaffHrs']");
             var Val2 = $("[id$='_txtOtherShipPdOtherStaffHrs']");
             var Val3 = $("[id$='_txtOtherInKindPdOtherStaffHrs']");
             var Total = $("[id$='_txtOtherTotalOtherStaffHrs']");
             CalcVertical(Val1, Val2, Val3, Total);
             CalcSect3DEF();

         })

         $("[id$='_txtOtherInKindPdOtherStaffHrs']").blur(function() {

             var Val1 = $("[id$='_txtOtherVolOtherStaffHrs']");
             var Val2 = $("[id$='_txtOtherShipPdOtherStaffHrs']");
             var Val3 = $("[id$='_txtOtherInKindPdOtherStaffHrs']");
             var Total = $("[id$='_txtOtherTotalOtherStaffHrs']");
             CalcVertical(Val1, Val2, Val3, Total);
             CalcSect3DEF();

         })



         //*******************Section 4************************
         //         $("[id$='_txtInitTrainings']").blur(function() {

         //         var Val1 = $("[id$='_txtInitTrainings']");
         //         var Val2 = $("[id$='_txtInitTrainingsAttend']");
         //         var Total = $("[id$='_txtInitTrainingsTotalHrs']");
         //             Calc(Val1, Val2, Total);

         //         })

         //         $("[id$='_txtInitTrainingsAttend']").blur(function() {
         //             var Val1 = $("[id$='_txtInitTrainings']");
         //             var Val2 = $("[id$='_txtInitTrainingsAttend']");
         //             var Total = $("[id$='_txtInitTrainingsTotalHrs']");
         //             Calc(Val1, Val2, Total);
         //         })

         //         $("[id$='_txtInitTrainingsTotalHrs']").blur(function() {
         //             var Val1 = $("[id$='_txtInitTrainings']");
         //             var Val2 = $("[id$='_txtInitTrainingsAttend']");
         //             var Total = $("[id$='_txtInitTrainingsTotalHrs']");
         //             Calc(Val1, Val2, Total);
         //         })



         //         $("[id$='_txtUpdtTrainings']").blur(function() {
         //             var Val1 = $("[id$='_txtUpdtTrainings']");
         //             var Val2 = $("[id$='_txtUpdtTrainingsAttend']");
         //             var Total = $("[id$='_txtUpdtTrainingsTotalHrs']");
         //             Calc(Val1, Val2, Total);
         //         })

         //         $("[id$='_txtUpdtTrainingsAttend']").blur(function() {
         //             var Val1 = $("[id$='_txtUpdtTrainings']");
         //             var Val2 = $("[id$='_txtUpdtTrainingsAttend']");
         //             var Total = $("[id$='_txtUpdtTrainingsTotalHrs']");
         //             Calc(Val1, Val2, Total);
         //         })

         //         $("[id$='_txtUpdtTrainingsTotalHrs']").blur(function() {
         //             var Val1 = $("[id$='_txtUpdtTrainings']");
         //             var Val2 = $("[id$='_txtUpdtTrainingsAttend']");
         //             var Total = $("[id$='_txtUpdtTrainingsTotalHrs']");
         //             Calc(Val1, Val2, Total);
         //         })
         //         



     });
     //***********************************FUNCTIONS**********************
     function CalcCompleteTotalABC() {
         var Val1 = $("[id$='_txtStateTotalCoun']");
         var Val2 = $("[id$='_txtOtherTotalCoun']");
         var Total = $("[id$='_txtTotalTotalCoun']");
         Calc(Val1, Val2, Total);

     }

     function CalcSect3ABC() {
         var Val1 = $("[id$='_txtStateTotalOtherStaff']");
         var Val2 = $("[id$='_txtOtherTotalOtherStaff']");
         var Total = $("[id$='_txtTotalTotalOtherStaff']");
         Calc(Val1, Val2, Total);
     }

     function CalcSect3DEF() {
         //txtTotalTotalCounHrs
         var Val1 = $("[id$='_txtStateTotalOtherStaffHrs']");
         var Val2 = $("[id$='_txtOtherTotalOtherStaffHrs']");
         var Total = $("[id$='_txtTotalTotalOtherStaffHrs']");
         Calc(Val1, Val2, Total);
     }


     function CalcCompleteTotalDEF() {

         var Val1 = $("[id$='_txtStateTotalCounHrs']");
         if (Val1.attr("value") == "")
             Val1.val("0");

         var Val2 = $("[id$='_txtOtherTotalCounHrs']");
         if (Val2.attr("value") == "")
             Val2.val("0");


         var Total = $("[id$='_txtTotalTotalCounHrs']");
         Calc(Val1, Val2, Total);
     }

     function Calc(ctrl1, ctrl2, TotalField) {
         var Val1 = 0;
         var Val2 = 0;
         var Total = parseInt("0");
         if (ctrl1.attr("value") != "") {

             Val1 = parseInt(ctrl1.attr("value").replace(/^[ 0]/g, ''));
             if (Val1.toString() == "NaN")
                 Val1 = parseInt("0");

         }

         if (ctrl2.attr("value") != "");
         {
             Val2 = parseInt(ctrl2.attr("value").replace(/^[ 0]/g, ''));
             if (Val2.toString() == "NaN")
                 Val2 = parseInt("0");

         }

         if (Total.toString() == "NaN") {
             Total = parseInt("0");
         }
         Total = Val1 + Val2;

         TotalField.val(Total.toString());
     }

     function CalcVertical(ctrl1, ctrl2, ctrl3, TotalField) {
         var Val1 = 0;
         var Val2 = 0;
         var Val3 = 0;
         var Total = 0;
         if (ctrl1.attr("value") != "") {

             Val1 = parseInt(ctrl1.attr("value").replace(/^[ 0]/g, ''));
             if (Val1.toString() == "NaN")
                 Val1 = 0;
         }

         if (ctrl2.attr("value") != "");
         {
             Val2 = parseInt(ctrl2.attr("value").replace(/^[ 0]/g, ''));
             if (Val2.toString() == "NaN")
                 Val2 = 0;
         }

         if (ctrl3.attr("value") != "");
         {
             Val3 = parseInt(ctrl3.attr("value").replace(/^[ 0]/g, ''));
             if (Val3.toString() == "NaN")
                 Val3 = 0;
         }

         Total = Val1 + Val2 + Val3;
         if (Total.toString() == "NaN") {
             Total = 0;
         }
         TotalField.val(Total.toString());

     }
   </SCRIPT>
   <asp:FormView runat="server" ID="formViewResourceReport" DefaultMode="Insert" DataSourceID="dataSourceViewResourceReport"  Width="100%">
    <InsertItemTemplate>
            <table border="0" cellpadding="0" cellspacing="0" width="100%" style="padding-left: 10px;
                padding-right: 10px;">
                <tr>
                    <td align="center" colspan="2">
                        <h1>
                            RESOURCE REPORT</h1>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Complete Only One RR Form for the Entire State. Do Not Submit Sponsoring-Agency-Level or Within-State-Regional Resource Reports.
                        All Person Counts Should Reflect Active Counselors, Coordinators, Other Staff as of the End of Each Grant Year&nbsp;(31 March).
                        
                    </td>
                </tr>
                <tr><td colspan="2"><hr /></td></tr>
                <tr>
                    <td valign="top" colspan="2">
                        <table width="100%">
                            <tr>
                                <td width="45%" colspan="2">
                                    <div><asp:Label id="MsgFeedBack" runat="server"  visible="false"></asp:Label></div>
                                    <strong>12 Month Period for This Report</strong>
                                <td width="20%">
                                    <strong>State Code</strong>
                                </td>
                                <td width="35%">
                                    <strong>State Grantee Name</strong>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" width="20%">
                                    From:&nbsp;<asp:DropDownList  ID="cmbFromDate" runat="server"  Width="100px" ToolTip="From"></asp:DropDownList>
                                </td>
                                <td valign="top">
                                    To:&nbsp;<asp:DropDownList  ID="cmbToDate"  runat="server" Width="100px" ToolTip="To"></asp:DropDownList>
                                </td>
                                <td valign="top">
                                    <asp:TextBox   ID="txtStateCode" runat="server" Width="50px" Text='<%# Bind("StateFIPSCode") %>' ToolTip="State Code"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox   ID="txtStateGranteeName"   runat="server" Width="150px" MaxLength="255"   Text='<%# Bind("StateGranteeName") %>' ToolTip="State Grantee Name"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table width="100%">
                            <tr>
                                <td valign="top" width="30%">
                                    <strong>Person Completing Report</strong>
                                </td>
                                <td valign="top" width="30%">
                                    <strong>Title</strong>
                                </td>
                                <td valign="top" width="40%">
                                    <strong>Telephone Number</strong>
                                    <br/>
                                    <pp:PropertyProxyValidator ID="proxyValidatorPrimaryPhone" runat="server" ControlToValidate="txtTelephone" CssClass="required" Display="Dynamic" DisplayMode="List" PropertyName="PrimaryPhone" RulesetName="Data" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" OnValueConvert="proxyValidatorPhoneNumber_ValueConvert" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:TextBox   ID="txtPersonCompletingReport" runat="server" Text='<%# Bind("PersonCompletingReportName") %>' ToolTip="Person Completing Report"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox    ID="txtTitle" runat="server" MaxLength="50" Text='<%# Bind("Title") %>' ToolTip="Title" ></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox   ID="txtTelephone" runat="server" Text='<%# Bind("PersonCompletingReportTel") %>' ToolTip="Telephone Number"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <strong>Section 1</strong>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <strong>Number of Active Counselors And Hours As of 31 March </strong>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td valign="top" width="40%">
                                </td>
                                <td valign="top" width="15%">
                                    State Office
                                </td>
                                <td valign="top" width="15%">
                                    All Other Local<br />
                                    and Field Sites
                                </td>
                                <td valign="top" width="30%">
                                    Total
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    A. Number of Volunteer Counselors
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter"  ID="txtStateVolCoun" runat="server" Width="50px" Text='<%# Bind("NoOfStateVolunteerCounselors") %>'  onkeypress="return isNumberKey(event)" ToolTip="State Office" ></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" ID="txtOtherVolCoun" runat="server" Width="50px" Text='<%# Bind("NoOfOtherVolunteerCounselors") %>' onkeypress="return isNumberKey(event)" ToolTip=" All Other Local and Field Sites"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter" readonly="true" ID="txtTotalVolCoun" runat="server" Width="50px" Text='<%# Bind("TotalNumberOfVolCounselors") %>'  onkeypress="return isNumberKey(event)" ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    B. Number of SHIP-Paid Counselors
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter"  ID="txtStateShipPdCoun" runat="server" Width="50px" Text='<%# Bind("NoOfStateShipPaidCounselors") %>'   onkeypress="return isNumberKey(event)"  ToolTip="State Office"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" ID="txtOtherShipPdCoun" runat="server" Width="50px"  Text='<%# Bind("NoOfOtherShipPaidCounselors") %>'  onkeypress="return isNumberKey(event)"  ToolTip=" All Other Local and Field Sites"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" readonly="true" ID="txtTotalShipPdCoun" runat="server" Width="50px" Text='<%# Bind("TotalNoOfShipPaidCounselors") %>'  onkeypress="return isNumberKey(event)"  ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    C. Number of In-Kind-Paid Counselors
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" ID="txtStateInKindPdCoun" runat="server" Width="50px" Text='<%# Bind("NoOfStateInKindPaidCounselors") %>'  onkeypress="return isNumberKey(event)" ToolTip="State Office"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter"  ID="txtOtherInKindPdCoun" runat="server" Width="50px" Text='<%# Bind("NoOfOtherInKindPaidCounselors") %>'  onkeypress="return isNumberKey(event)" ToolTip=" All Other Local and Field Sites"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" readonly="true" ID="txtTotalInKindPdCoun" runat="server" Width="50px"  Text='<%# Bind("TotalNoOfInKindPaidCounselors") %>' onkeypress="return isNumberKey(event)"  ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Total Number of Counselors - A+B+C
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter" readonly="true"  ID="txtStateTotalCoun" runat="server" Width="50px"  Text='<%# Bind("TotalNoOfCounselorsABCState") %>'  onkeypress="return isNumberKey(event)"  ToolTip="State Office"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter" readonly="true"  ID="txtOtherTotalCoun" runat="server" Width="50px"  Text='<%# Bind("TotalNoOfCounselorsABCOther") %>' onkeypress="return isNumberKey(event)"  ToolTip=" All Other Local and Field Sites"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter"   ID="txtTotalTotalCoun" runat="server" Width="50px"  Text='<%# Bind("TotalNoOfCounselorsABCStateAndOther") %>' onkeypress="return isNumberKey(event)"  ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    D. Volunteer Counselor Hours
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter"  ID="txtStateVolCounHrs" runat="server" Width="50px" Text='<%# Bind("NoOfStateVolunteerCounselorsHrs") %>'   onkeypress="return isNumberKey(event)"  ToolTip="State Office"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" ID="txtOtherVolCounHrs" runat="server" Width="50px"  Text='<%# Bind("NoOfOtherVolunteerCounselorsHrs") %>'   onkeypress="return isNumberKey(event)"  ToolTip=" All Other Local and Field Sites"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" readonly="true" ID="txtTotalVolCounHrs" runat="server" Width="50px"  Text='<%# Bind("TotalNoOfCounselorHours") %>'  onkeypress="return isNumberKey(event)"  ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    E. SHIP-Paid Counselor Hours
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" ID="txtStateShipPdCounHrs" runat="server" Width="50px"  Text='<%# Bind("NoOfStateShipPaidCounselorsHrs") %>'   onkeypress="return isNumberKey(event)"  ToolTip="State Office"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" ID="txtOtherShipPdCounHrs"  runat="server" Width="50px" Text='<%# Bind("NoOfOtherShipPaidCounselorsHrs") %>'  onkeypress="return isNumberKey(event)"  ToolTip=" All Other Local and Field Sites"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" readonly="true" ID="txtTotalShipPdCounHrs" runat="server" Width="50px"  Text='<%# Bind("TotalShipPaidCounselorsHours") %>'  onkeypress="return isNumberKey(event)"  ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    F. In-Kind-Paid Counselors Hours
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" ID="txtStateInKindPdHrs" runat="server" Width="50px"  Text='<%# Bind("NoOfStateInKindPaidCounselorsHrs") %>'  onkeypress="return isNumberKey(event)"  ToolTip="State Office"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter"  ID="txtOtherInKindPdHrs" runat="server" Width="50px"  Text='<%# Bind("NoOfOtherInKindPaidCounselorsHrs") %>'  onkeypress="return isNumberKey(event)"  ToolTip=" All Other Local and Field Sites"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" readonly="true" ID="txtTotalInKindPdHrs" runat="server" Width="50px"  Text='<%# Bind("TotalInKindPdHrs") %>'  onkeypress="return isNumberKey(event)"  ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Total Counselors Hours - D+E+F
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" readonly="true" ID="txtStateTotalCounHrs" runat="server" Width="50px"  Text='<%# Bind("TotalCounselorsHrsDEFState") %>'   onkeypress="return isNumberKey(event)"  ToolTip="State Office"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter"  readonly="true" ID="txtOtherTotalCounHrs" runat="server" Width="50px" Text='<%# Bind("TotalCounselorsHrsDEFOther") %>'     onkeypress="return isNumberKey(event)"  ToolTip=" All Other Local and Field Sites"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" readonly="true" ID="txtTotalTotalCounHrs" runat="server" Width="50px" Text='<%# Bind("TotalCounselorsHrsDEFStateAndOther") %>'  onkeypress="return isNumberKey(event)"  ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <strong>Section 2</strong>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <strong>Number of Local Coordinators / Sponsors and Hours As of 31 March</strong>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    &nbsp;
                                </td>
                                <td>
                                    Total
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    A. Number of Volunteer (Unpaid) Coordinators
                                </td>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter"  ID="txtTotalUnPdCoord" runat="server" Width="50px" Text='<%# Bind("NoOfUnpaidVolunteerCoordinators") %>'    onkeypress="return isNumberKey(event)" ToolTip="Number of Volunteer (Unpaid) Coordinators Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    B. Number of SHIP-Paid Coordinators
                                </td>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" ID="txtTotalShipPdCoord" runat="server" Width="50px"  Text='<%# Bind("NoOfSHIPPaidCoordinators") %>'   onkeypress="return isNumberKey(event)" ToolTip="Number of SHIP-Paid Coordinators Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    C. Number of In-Kind-Paid Coordinators
                                </td>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" ID="txtTotalInKindPdCoord" runat="server" Width="50px"  Text='<%# Bind("NoOfInKindPaidCoordinators") %>'   onkeypress="return isNumberKey(event)" ToolTip="Number of In-Kind-Paid Coordinators Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Total Number of Coordinators - A+B+C
                                </td>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter" readonly="true"  ID="txtTotalCoord" runat="server" Width="50px" Text='<%# Bind("TotalNoOfCoordinators") %>'  onkeypress="return isNumberKey(event)" ToolTip="Total Number of Coordinators - A+B+C Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    D. Volunteer (Unpaid) Coordinator Hours
                                </td>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" ID="txtTotalUnPdCoordHrs" runat="server" Width="50px"  Text='<%# Bind("NoOfUnpaidVolunteerCoordinatorsHrs") %>'    onkeypress="return isNumberKey(event)" ToolTip="Volunteer (Unpaid) Coordinator Hours Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    E. SHIP-Paid Coordinator Hours
                                </td>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" ID="txtTotalShipPdCoordHrs" runat="server" Width="50px"  Text='<%# Bind("NoOfSHIPPaidCoordinatorsHrs") %>'  onkeypress="return isNumberKey(event)" ToolTip="SHIP-Paid Coordinator Hours Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    F. In-Kind-Paid Coordinator Hours
                                </td>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" ID="txtTotalInKindPdCoordHrs" runat="server" Width="50px" Text='<%# Bind("NoOfInKindPaidCoordinatorsHrs") %>'   onkeypress="return isNumberKey(event)" ToolTip="In-Kind-Paid Coordinator Hours Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Total Coordinator Hours - D+E+F
                                </td>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter" readonly="true" ID="txtTotalCoordHrs" runat="server" Width="50px"  Text='<%# Bind("TotalCoordiantorsHrsDEF") %>'    onkeypress="return isNumberKey(event)" ToolTip="Total Coordinator Hours - D+E+F Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <strong>Section 3</strong>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <strong>Number of Other Paid and Volunteer Staff And Hours As of 31 March </strong>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" width="40%">
                                </td>
                                <td valign="top" width="15%">
                                    State Office
                                </td>
                                <td valign="top" width="15%">
                                    All Other Local<br />
                                    and Field Sites
                                </td>
                                <td valign="top" width="30%">
                                    Total
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    A. Number of Volunteer Other Staff
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" ID="txtStateVolOtherStaff" runat="server" Width="50px"  Text='<%# Bind("NoOfStateVolunteerOtherStaff") %>'   onkeypress="return isNumberKey(event)"  ToolTip="State Office"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter"  ID="txtOtherVolOtherStaff" runat="server" Width="50px" Text='<%# Bind("NoOfOtherVolunteerOtherStaff") %>'  onkeypress="return isNumberKey(event)"  ToolTip=" All Other Local and Field Sites"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter" readonly="true" ID="txtTotalVolOtherStaff" runat="server" Width="50px"  Text='<%# Bind("TotalNoOfOtherVolunteerOtherStaffStateAndOther") %>' onkeypress="return isNumberKey(event)" ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    B. Number of SHIP-Paid Other Staff
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter"  ID="txtStateShipPdOtherStaff" runat="server" Width="50px" Text='<%# Bind("NoOfStateShipPaidOtherStaff") %>'   onkeypress="return isNumberKey(event)"  ToolTip="State Office"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter"  ID="txtOtherShipPdOtherStaff" runat="server" Width="50px" Text='<%# Bind("NoOfOtherShipPaidOtherStaff") %>'  onkeypress="return isNumberKey(event)"  ToolTip=" All Other Local and Field Sites"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter" readonly="true" ID="txtTotalShipPdOtherStaff" runat="server" Width="50px"  Text='<%# Bind("TotalNoOfShipPaidStateAndOtherStaff") %>'   onkeypress="return isNumberKey(event)" ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    C. Number of In-Kind-Paid Other Staff
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter"  ID="txtStateInKindPdOtherStaff" runat="server" Width="50px"  Text='<%# Bind("NoOfStateInKindPaidOtherStaff") %>'   onkeypress="return isNumberKey(event)"  ToolTip="State Office"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" ID="txtOtherInKindPdOtherStaff" runat="server" Width="50px" Text='<%# Bind("NoOfOtherInKindPaidOtherStaff") %>'   onkeypress="return isNumberKey(event)"  ToolTip=" All Other Local and Field Sites"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter" readonly="true"  ID="txtTotalInKindPdOtherStaff" runat="server" Width="50px"   Text='<%# Bind("TotalNoOfOtherInKindPaidStateAndOtherStaff") %>'   onkeypress="return isNumberKey(event)" ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Total Number of Other Staff - A+B+C
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter" readonly="true"  ID="txtStateTotalOtherStaff" runat="server" Width="50px" Text='<%# Bind("TotalNoOfOtherInKindPaidStaffStateABC") %>'   onkeypress="return isNumberKey(event)"  ToolTip="State Office"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" readonly="true" ID="txtOtherTotalOtherStaff" runat="server" Width="50px"  Text='<%# Bind("TotalNoOfOtherInKindPaidStaffOtherABC") %>'  onkeypress="return isNumberKey(event)"  ToolTip=" All Other Local and Field Sites"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter" readonly="true"  ID="txtTotalTotalOtherStaff" runat="server" Width="50px"  Text='<%# Bind("TotalNoOfOtherInKindPaidStaffStateAndOtherABC") %>'  onkeypress="return isNumberKey(event)" ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    D. Volunteer Other Staff Hours
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter"  ID="txtStateVolOtherStaffHrs" runat="server" Width="50px" Text='<%# Bind("NoOfStateVolunteerOtherStaffHrs") %>'   onkeypress="return isNumberKey(event)"  ToolTip="State Office"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" ID="txtOtherVolOtherStaffHrs" runat="server" Width="50px" Text='<%# Bind("NoOfOtherVolunteerOtherStaffHrs") %>'   onkeypress="return isNumberKey(event)"  ToolTip=" All Other Local and Field Sites"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" readonly="true" ID="txtTotalVolOtherStaffHrs" runat="server" Width="50px"  Text='<%# Bind("TotalNoOfVolunteerOtherStaffHrsStateAndOther") %>' TotalNoOfVolunteerOtherStaffHrsStateAndOther  onkeypress="return isNumberKey(event)" ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    E. SHIP-Paid Other Staff Hours
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" ID="txtStateShipPdOtherStaffHrs" runat="server" Width="50px"  Text='<%# Bind("NoOfStateShipPaidOtherStaffHrs") %>'   onkeypress="return isNumberKey(event)"  ToolTip="State Office"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter"  ID="txtOtherShipPdOtherStaffHrs" Text='<%# Bind("NoOfOtherShipPaidOtherStaffHrs") %>'  runat="server" Width="50px"  onkeypress="return isNumberKey(event)"  ToolTip=" All Other Local and Field Sites"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter" readonly="true" ID="txtTotalShipPdOtherStaffHrs" runat="server" Width="50px" Text='<%# Bind("TotalNoOfSHIPPaidStaffHrsStateAndOther") %>'    onkeypress="return isNumberKey(event)" ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    F. In-Kind-Paid Other Staff Hours
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" ID="txtStateInKindPdOtherStaffHrs" runat="server" Width="50px" Text='<%# Bind("NoOfStateInKindPaidOtherStaffHrs") %>'    onkeypress="return isNumberKey(event)"  ToolTip="State Office"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" ID="txtOtherInKindPdOtherStaffHrs" runat="server" Width="50px" Text='<%# Bind("NoOfOtherInKindPaidOtherStaffHrs") %>'   onkeypress="return isNumberKey(event)"  ToolTip=" All Other Local and Field Sites"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" readonly="true"  ID="txtTotalInKindPdOtherStaffHrs" runat="server" Width="50px" Text='<%# Bind("TotalNoOfOtherInKindPaidStateAndOtherHrs") %>'   onkeypress="return isNumberKey(event)" ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Total Other Staff Hours - D+E+F
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" readonly="true"  ID="txtStateTotalOtherStaffHrs" runat="server" Width="50px"  Text='<%# Bind("TotalNoOfOtherInKindPaidStateDEF") %>'   onkeypress="return isNumberKey(event)"  ToolTip="State Office"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" readonly="true"  ID="txtOtherTotalOtherStaffHrs" runat="server" Width="50px"  Text='<%# Bind("TotalNoOfOtherInKindPaidOtherDEF") %>'  onkeypress="return isNumberKey(event)"  ToolTip=" All Other Local and Field Sites"></asp:TextBox>
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" readonly="true"  ID="txtTotalTotalOtherStaffHrs" runat="server" Width="50px"  Text='<%# Bind("TotalNoOfOtherInKindPaidOtherDEFStateAndOther") %>'  onkeypress="return isNumberKey(event)" ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <strong>Section 4</strong>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <strong>Counselor Trainings </strong>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    &nbsp;
                                </td>
                                <td>
                                    Total
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="2">
                                    A. Number of Initial Trainings for New SHIP Counselors
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter"  ID="txtInitTrainings" runat="server" Width="50px" Text='<%# Bind("NoOfInitialTrainings") %>'  onkeypress="return isNumberKey(event)" ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="2">
                                    B. Number of New SHIP Counselors Attending Initial Trainings
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter"  ID="txtInitTrainingsAttend" runat="server" Width="50px" Text='<%# Bind("NoOfInitialTrainingsAttend") %>'   onkeypress="return isNumberKey(event)" ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="2">
                                    C. Total Number of Counselor Hours in Initial Trainings
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter"  ID="txtInitTrainingsTotalHrs" runat="server" Width="50px" Text='<%# Bind("NoOfInitTrainingsTotalHrs") %>'  onkeypress="return isNumberKey(event)" ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="2">
                                    D. Number of Update Trainings for SHIP Counselors
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter"  ID="txtUpdtTrainings" runat="server" Width="50px" Text='<%# Bind("NoOfUpdateTrainings") %>'   onkeypress="return isNumberKey(event)" ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="2">
                                    E. Number of SHIP Counselors Attending Update Trainings
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:TextBox CssClass="tabEnter"  ID="txtUpdtTrainingsAttend" runat="server" Width="50px"  Text='<%# Bind("NoOfUpdateTrainingsAttend") %>' onkeypress="return isNumberKey(event)" ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="2">
                                    F. Total Number of Counselor Hours in Update Trainings
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:TextBox  CssClass="tabEnter" ID="txtUpdtTrainingsTotalHrs" runat="server" Width="50px" Text='<%# Bind("NoOfUpdateTrainingsTotalHrs") %>'  onkeypress="return isNumberKey(event)" ToolTip="Total"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <hr />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <strong>Section 5
                            <br />
                            <br />
                            Number of Total Active Counselors (SHIP-Paid, In-Kind-Paid and Volunteer Counselors)
                            with the Following Characteristics </strong>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" valign="top">
                        <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td valign="top" width="30%">
                                    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td valign="top">
                                                <strong>Years of SHIP Service</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Less Than 1 Year
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox CssClass="tabEnter"  ID="txtYrsServiceLessThan1" ToolTip="Less Than 1 Year" runat="server" Width="40px" Text='<%# Bind("NoOfYrsServiceLessThan1") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                1 Year Up to 3 Years
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtYrsService1To3" ToolTip="1 Year Up to 3 Years"  runat="server" Width="40px" Text='<%# Bind("NoOfYrsService1To3") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                3 Years Up to 5 Years
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtYrsService3To5" ToolTip="3 Years Up to 5 Years"  runat="server" Width="40px" Text='<%# Bind("NoOfYrsService3To5") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                More Than 5 Years
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtYrsServiceOver5" ToolTip="More Than 5 Years"  runat="server" Width="40px" Text='<%# Bind("NoOfYrsServiceOver5") %>'   onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Not Collected
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtYrsServiceNotCol" ToolTip=" Not Collected"  runat="server" Width="40px" Text='<%# Bind("NoOfYrsServiceNotCol") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Counselor Age</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Less Than 65 Years of Age
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox CssClass="tabEnter"  ID="txtAgeLessThan65" ToolTip="Less Than 65 Years of Age" runat="server" Width="40px" Text='<%# Bind("NoOfAgeLessThan65") %>'    onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                65 Years or Older
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtAgeOver65" ToolTip="65 Years or Older"  runat="server" Width="40px" Text='<%# Bind("NoOfAgeOver65") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Not Collected
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtAgeNotCol" ToolTip="Not Collected"  runat="server" Width="40px" Text='<%# Bind("NoOfAgeNotCollected") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Counselor Gender</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Female
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtGenderFemale" ToolTip="Female" runat="server" Width="40px" Text='<%# Bind("NoOfGenderFemale") %>'   onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Male
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtGenderMale"  ToolTip="Male" runat="server" Width="40px" Text='<%# Bind("NoOfGenderMale") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Not Collected
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtGenderNotCol" ToolTip=" Not Collected"  runat="server" Width="40px" Text='<%# Bind("NoOfGenderNotCollected") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" width="35%">
                                    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" style="padding-left: 15px;
                                        padding-right: 10px;">
                                        <tr>
                                            <td>
                                                <strong>Counselor Race - Ethnicity</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Hispanic, Latino, or Spanish Origin
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtRaceHispanicLatSpa" ToolTip="Hispanic, Latino, or Spanish Origin"  runat="server" Width="40px" Text='<%# Bind("NoOfEthnicityHispanic") %>'   onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                White, Non-Hispanic
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtRaceWhite"  ToolTip="White, Non-Hispanic"  runat="server" Width="40px" Text='<%# Bind("NoOfEthnicityWhite") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Black, African American
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtRaceAfAm"  ToolTip="Black, African American"  runat="server" Width="40px" Text='<%# Bind("NoOfEthnicityAfricanAmerican") %>'   onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                American Indian or Alaska Native
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtRaceNative"  ToolTip="American Indian or Alaska Native"  runat="server" Width="40px" Text='<%# Bind("NoOfEthnicityAmericanIndian") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Asian Indian
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox CssClass="tabEnter"  ID="txtRaceAsian" ToolTip="Asian Indian"   runat="server" Width="40px" Text='<%# Bind("NoOfEthnicityAsianIndian") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Chinese
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox CssClass="tabEnter"  ID="txtRaceChinese"  ToolTip="Chinese"  runat="server" Width="40px"  Text='<%# Bind("NoOfEthnicityChinese") %>'   onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Filipino
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtRaceFilipino"  ToolTip="Filipino"  runat="server" Width="40px" Text='<%# Bind("NoOfEthnicityFilipino") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Japanese
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtRaceJapanese"  ToolTip="Japanese"  runat="server" Width="40px" Text='<%# Bind("NoOfEthnicityJapanese") %>'   onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Korean
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtRaceKorean"  ToolTip="Korean"  runat="server" Width="40px" Text='<%# Bind("NoOfEthnicityKorean") %>'   onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Vietnamese
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtRaceVietnamese"  ToolTip="Vietnamese"  runat="server" Width="40px" Text='<%# Bind("NoOfEthnicityVietnamese") %>'   onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Native Hawaiian
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtRaceHawaiian"  ToolTip="Native Hawaiian"  runat="server" Width="40px"  Text='<%# Bind("NoOfEthnicityNativeHawaiian") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Guamanian or Chamorro
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtRaceGuamanian"  ToolTip="Guamanian or Chamorro"  runat="server" Width="40px" Text='<%# Bind("NoOfEthnicityGuamanian") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Samoan
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtVietnameseSamoan"  ToolTip="Samoan"  runat="server" Width="40px" Text='<%# Bind("NoOfEthnicitySamoan") %>'   onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Other Asian
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtVietnameseOtrAsian"  ToolTip="Other Asian"  runat="server" Width="40px" Text='<%# Bind("NoOfEthnicityOtherAsian") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Other Pacific Islander
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtRacePacIslander"  ToolTip="Other Pacific Islander"  runat="server" Width="40px" Text='<%# Bind("NoOfEthnicityOthherPacificIslander") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Some Other Race - Ethnicity
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtRaceOtherRace"  ToolTip=" Some Other Race - Ethnicity"  runat="server" Width="40px"  Text='<%# Bind("NoOfEthnicitySomeOtherRace") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                More Than One Race - Ethnicity
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox CssClass="tabEnter"  ID="txtRaceMoreThanOne"  ToolTip="More Than One Race - Ethnicity"  runat="server" Width="40px" Text='<%# Bind("NoOfEthnicityMoreThanOneRaceEthnicity") %>'   onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Not Collected
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtRaceNotCol"  ToolTip=" Not Collected"  runat="server" Width="40px" Text='<%# Bind("NoOfEthnicityNotCollected") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" width="32%">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <strong>Counselor Disability</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Disabled
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtDisabledTrue" ToolTip="Disabled" runat="server" Width="40px" Text='<%# Bind("NoOfDisabled") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Not Disabled
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtDisabledFalse" ToolTip=" Not Disabled"  runat="server" Width="40px" Text='<%# Bind("NoOfNotDisabled") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Not Collected
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox CssClass="tabEnter"  ID="txtCounselorDisabilityNotCollected"  ToolTip="Not Collected" runat="server" Width="40px" Text='<%# Bind("NoOfDisabledNotCollected") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <strong>Counselor Speaks Another Language</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Language Other than English
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtLangOther" ToolTip="Language Other than English"  runat="server" Width="40px" Text='<%# Bind("NoOfSpeaksAnotherLanguageOtherThanEnglish") %>'   onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                English Speaker Only
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtLangEnglish" ToolTip="English Speaker Only"  runat="server" Width="40px" Text='<%# Bind("NoOfEnglishSpeakerOnly") %>'  onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Not Collected
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox  CssClass="tabEnter" ID="txtLangNotCol" ToolTip="Not Collected"  runat="server" Width="40px" Text='<%# Bind("NoOfSpeaksAnotherLanguageNotCollected") %>'   onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="float:right">
                            <asp:Button ID="btnSave" CssClass="formbutton1" Width="105px" runat="server" 
                                Text="Save" onclick="btnSave_Click"/>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
               
               
            </table>
        </InsertItemTemplate>
  </asp:FormView>

    <pp:ObjectContainerDataSource ID="dataSourceViewResourceReport" runat="server" 
        DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.ViewResourceReportViewData" 
        oninserted="dataSourceViewResourceReport_Inserted" 
        oninserting="dataSourceViewResourceReport_Inserting">
        
        </pp:ObjectContainerDataSource>
</asp:Content>
