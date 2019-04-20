<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterPage/MstPage_L0.master" AutoEventWireup="true" CodeFile="weblist.aspx.cs" Inherits="Mapping_MappingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="div-artlist">
        <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="itemHolder" DataSourceID="SqlDataSource1">
            <%-- 布局模板 --%>
            <LayoutTemplate>
                <div style="float:left;width:620px;height:auto;margin-bottom:20px;background-color:#eaeaea"><p style="margin:0 auto;width:30px;font-weight:700;color:#6A5ACD">标题</p></div>
                <div style="float:right;width:60px;height:auto;margin-bottom:20px;background-color:#eaeaea"><p style="font-weight:700;color:#6A5ACD">发表时间</p></div>

                <div runat="server" id="itemHolder"></div>
            </LayoutTemplate>

            <ItemTemplate>
                <div class="divlistitem">
                    <div class="fl">
                        <div class="divlistitem_head"></div><a href='webdetail.aspx?order=<%#Eval("ArtOrder") %>&type=<%#Eval("ArtType") %>'><%#Eval("ArtTitle") %></a>
                    </div>
                    <div style="float:right;"><p><%#Eval("ArtDate","{0:D}") %></p></div>
                    <div class="clr"></div>
                </div>
            </ItemTemplate>
        </asp:ListView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:qds114476253_dbConnectionString %>" SelectCommand="SELECT * FROM [T_ARTICLE] WHERE ([ArtType] = @ArtType)">
            <SelectParameters>
                <asp:Parameter DefaultValue="2" Name="ArtType" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>

        <div class="divPager">
            <asp:DataPager ID="DataPager1" runat="server"
                PagedControlID="ListView1" PageSize="12" QueryStringField="pagesize">
                <Fields>
                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" FirstPageText="首页" />
                    <asp:NumericPagerField />
                    <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" LastPageText="尾页"/>
                </Fields>
            </asp:DataPager>
            <div class="clr"></div>
        </div>
    </div>
    <div class="div-other"></div>

    <div class="clr"></div>
</asp:Content>

