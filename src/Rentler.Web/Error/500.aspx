<%@ Page Title="Oops!" Language="C#" MasterPageFile="~/Error/Error.Master" AutoEventWireup="true" CodeBehind="500.aspx.cs" Inherits="Rentler.Web.Error._500" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTitle" runat="server">Rentler - Error</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<div class="centerHeading">
	<h1>Oops! Something went wrong.</h1>
	<div>An error was thrown, but we have reported it to our developers.</div>
    <div style="margin-top: 15px;"><a href="javascript:history.go(-1)">Click here to go back</a></div>
</div>
</asp:Content>
