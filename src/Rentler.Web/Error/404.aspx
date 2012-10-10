<%@ Page Title="" Language="C#" MasterPageFile="~/Error/Error.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="Rentler.Web.Error._404" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphTitle" runat="server">Rentler - Not Found</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <div class="centerHeading">
	    <h1>Not Found</h1>
	    <div>We're sorry we couldn't find what you were looking for.</div>
        <div style="margin-top: 15px;"><a href="javascript:history.go(-1)">Click here to go back</a></div>
    </div>
</asp:Content>
