<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterPage/MstPage_L0.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <%--图片轮播--%>
    <div class="div-lbt" id="show">
    </div>

    <%-- 最新文章--%>
    <div class="latestarticle">
        <%-- 分割线--%>
        <div class="dashed"></div>
        <%-- 头部--%>
        <div class="titlecode">
            <div></div>
            最新内容
        </div>

        <asp:ListView ID="lvLastedArticle" runat="server" ItemPlaceholderID="itemHolder">
            <%-- 布局模板 --%>
            <LayoutTemplate>
                <div runat="server" id="itemHolder"></div>
            </LayoutTemplate>

            <ItemTemplate>
                <div class="divlistitem">
                    <div style="float: left; width: 100%;">
                        <div class="divlistitem_head"></div>
                        <a target="_blank" href="dotnet/dotnetdetail.aspx?order=<%#Eval("ArtOrder") %>&type=<%#Eval("ArtType") %>"><%#Eval("ArtTitle") %></a>
                        <br />
                    </div>

                    <div class="clr"></div>
                </div>
            </ItemTemplate>
        </asp:ListView>

    </div>

    <div class="divarticle">
        <%--.NET文章区域 --%>
        <div class="listarticle">
            <div class="dashed"></div>
            <div class="titlecode">
                <div></div>
                .NET文章<a target="_blank" href="dotnet/dotnetlist.aspx" class="">更多</a>
            </div>

            <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="itemHolder" DataSourceID="SqlDataSource1">
                <%-- 布局模板 --%>
                <LayoutTemplate>
                    <div runat="server" id="itemHolder"></div>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="divlistitem">
                        <div style="float: left; width: 100%;">
                            <div class="divlistitem_head"></div>
                            <a target="_blank" href="dotnet/dotnetdetail.aspx?order=<%#Eval("ArtOrder") %>&type=<%#Eval("ArtType") %>"><%#Eval("ArtTitle") %></a>
                            <br />
                        </div>

                        <div class="clr"></div>
                    </div>
                </ItemTemplate>
            </asp:ListView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:qds114476253_dbConnectionString %>" SelectCommand="SELECT top(10) * FROM [T_ARTICLE] WHERE ([ArtType] = @ArtType) order by ArtDate desc">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="ArtType" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>

        <div class="listarticle">
            <div class="dashed"></div>
            <div class="titlecode">
                <div></div>
                Java文章<a target="_blank" href="java/javalist.aspx" class="">更多</a>
            </div>

            <asp:ListView ID="ListView2" runat="server" ItemPlaceholderID="itemHolder" DataSourceID="SqlDataSource2">
                <%-- 布局模板 --%>
                <LayoutTemplate>
                    <div runat="server" id="itemHolder"></div>
                </LayoutTemplate>

                <ItemTemplate>
                    <div class="divlistitem">
                        <div style="float: left; width: 100%;">
                            <div class="divlistitem_head"></div>
                            <a target="_blank" href="java/javadetail.aspx?order=<%#Eval("ArtOrder") %>&type=<%#Eval("ArtType") %>"><%#Eval("ArtTitle") %></a>
                            <br />
                        </div>

                        <div class="clr"></div>
                    </div>
                </ItemTemplate>
            </asp:ListView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:qds114476253_dbConnectionString %>" SelectCommand="SELECT top(10) * FROM [T_ARTICLE] WHERE ([ArtType] = @ArtType) order by ArtDate desc">
                <SelectParameters>
                    <asp:Parameter DefaultValue="1" Name="ArtType" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>

        </div>

        <div class="listarticle">
            <div class="dashed"></div>
            <div class="titlecode">
                <div></div>
                Web开发<a target="_blank" href="web/weblist.aspx" class="">更多</a>
            </div>

            <asp:ListView ID="ListView3" runat="server" ItemPlaceholderID="itemHolder" DataSourceID="SqlDataSource3">
                <%-- 布局模板 --%>
                <LayoutTemplate>
                    <div runat="server" id="itemHolder"></div>
                </LayoutTemplate>

                <ItemTemplate>
                    <div class="divlistitem">
                        <div style="float: left; width: 100%;">
                            <div class="divlistitem_head"></div>
                            <a target="_blank" href="web/webdetail.aspx?order=<%#Eval("ArtOrder") %>&type=<%#Eval("ArtType") %>"><%#Eval("ArtTitle") %></a>
                            <br />
                        </div>

                        <div class="clr"></div>
                    </div>
                </ItemTemplate>
            </asp:ListView>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:qds114476253_dbConnectionString %>" SelectCommand="SELECT top(10) * FROM [T_ARTICLE] WHERE ([ArtType] = @ArtType) order by ArtDate desc">
                <SelectParameters>
                    <asp:Parameter DefaultValue="2" Name="ArtType" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </div>

    <div class="divarticle">
        <div class="listarticle">
            <div class="dashed"></div>
            <div class="titlecode">
                <div></div>
                数据库<a target="_blank" href="database/databaselist.aspx" class="">更多</a>
            </div>

            <asp:ListView ID="ListView4" runat="server" ItemPlaceholderID="itemHolder" DataSourceID="SqlDataSource4">
                <%-- 布局模板 --%>
                <LayoutTemplate>
                    <div runat="server" id="itemHolder"></div>
                </LayoutTemplate>

                <ItemTemplate>
                    <div class="divlistitem">
                        <div style="float: left; width: 100%;">
                            <div class="divlistitem_head"></div>
                            <a target="_blank" href="database/databasedetail.aspx?order=<%#Eval("ArtOrder") %>&type=<%#Eval("ArtType") %>"><%#Eval("ArtTitle") %></a>
                            <br />
                        </div>

                        <div class="clr"></div>
                    </div>
                </ItemTemplate>
            </asp:ListView>
            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:qds114476253_dbConnectionString %>" SelectCommand="SELECT top(10) * FROM [T_ARTICLE] WHERE ([ArtType] = @ArtType) order by ArtDate desc">
                <SelectParameters>
                    <asp:Parameter DefaultValue="3" Name="ArtType" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>

        <div class="listarticle">
            <div class="dashed"></div>
            <div class="titlecode">
                <div></div>
                系统综合<a target="_blank" href="system/systemlist.aspx" class="">更多</a>
            </div>

            <asp:ListView ID="ListView5" runat="server" ItemPlaceholderID="itemHolder" DataSourceID="SqlDataSource5">
                <%-- 布局模板 --%>
                <LayoutTemplate>
                    <div runat="server" id="itemHolder"></div>
                </LayoutTemplate>

                <ItemTemplate>
                    <div class="divlistitem">
                        <div style="float: left; width: 100%;">
                            <div class="divlistitem_head"></div>
                            <a target="_blank" href="system/systemdetail.aspx?order=<%#Eval("ArtOrder") %>&type=<%#Eval("ArtType") %>"><%#Eval("ArtTitle") %></a>
                            <br />
                        </div>

                        <div class="clr"></div>
                    </div>
                </ItemTemplate>
            </asp:ListView>
            <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:qds114476253_dbConnectionString %>" SelectCommand="SELECT top(10) * FROM [T_ARTICLE] WHERE ([ArtType] = @ArtType) order by ArtDate desc">
                <SelectParameters>
                    <asp:Parameter DefaultValue="4" Name="ArtType" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>

        <div class="listarticle">
            <div class="dashed"></div>
            <div class="titlecode">
                <div></div>
                在线小工具<a target="_blank" href="tool/toollist.aspx" class="">更多</a>
            </div>
        </div>
    </div>

    <div class="clr"></div>
</asp:Content>

