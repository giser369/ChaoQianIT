﻿<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterPage/MstPage_L0.master" AutoEventWireup="true" CodeFile="systemdetail.aspx.cs" Inherits="system_Default2" %>

<%@ Register Src="~/App_Ctrls/Ctrl_ArtReader.ascx" TagPrefix="myCtrls" TagName="Ctrl_ArtReader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="div-artdetail">
        <myCtrls:Ctrl_ArtReader ID="newsReader_1" runat="server" ArtOrder="1" />
    </div>
    <div class="div-other"></div>
    <div class="clr"></div>
</asp:Content>

