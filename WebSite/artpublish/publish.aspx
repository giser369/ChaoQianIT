<%@ Page Language="C#" AutoEventWireup="true" CodeFile="publish.aspx.cs" Inherits="ExerciseCKEditor_Default" ValidateRequest="false" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <style>
        .arttitle { width: 600px; }
        .arttype { width: 600px; }
        .arteditor { width: 700px; }
        .artmanager { float: right; display: inline-block; width: 520px; height: auto; }

        .radiolist { display: inline-block; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="artmanager">
                文章管理
                <br />
                <asp:Button ID="btnDeleteArt" Text="删除文章" runat="server" OnClick="btnDeleteArt_Click" OnClientClick="return confirm('确认删除所选的文章吗？');" />
                <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" Text="全选" OnCheckedChanged="chkSelectAll_CheckedChanged"></asp:CheckBox>
                <asp:RadioButtonList ID="radioList" runat="server" RepeatDirection="Horizontal" CssClass="radiolist">
                    <asp:ListItem Selected="True">新增文章</asp:ListItem>
                    <asp:ListItem>编辑文章</asp:ListItem>
                </asp:RadioButtonList>

                <%-- GridView数据 --%>
                <asp:GridView ID="gvArtManager" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" PageSize="20" AllowSorting="True" OnSelectedIndexChanged="gvArtManager_SelectedIndexChanged" DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>
                    <Columns>

                        <asp:TemplateField HeaderText="选择">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCheck" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ItemStyle-Width="150px" ReadOnly="True" Visible="False">
                            <ItemStyle Width="150px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ArtType" HeaderText="ArtType" SortExpression="ArtType" Visible="False" />
                        <asp:BoundField DataField="ArtTitle" HeaderText="标题" SortExpression="ArtTitle" ItemStyle-Width="150px">
                            <ItemStyle Width="150px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ArtDate" HeaderText="日期" SortExpression="ArtDate" />
                        <asp:BoundField DataField="ArtContent" HeaderText="ArtContent" SortExpression="ArtContent" Visible="False" />
                        <asp:BoundField DataField="ArtOrder" HeaderText="编号" SortExpression="ArtOrder" />
                        <asp:BoundField DataField="ArtGuide" HeaderText="ArtGuide" SortExpression="ArtGuide" Visible="False" />
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerSettings FirstPageText="第一页" LastPageText="最后一页" NextPageText="下一页" PreviousPageText="上一页" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:qds114476253_dbConnectionString %>"></asp:SqlDataSource>
            </div>
            <div class="arttitle">
                文章标题：<asp:TextBox ID="txtArtTitle" Width="400px" runat="server"></asp:TextBox>
            </div>

            <div class="arttype">
                文章类型：<asp:DropDownList ID="ddlArtType" Width="200px" runat="server" OnSelectedIndexChanged="ddlArtType_SelectedIndexChanged"
                    AutoPostBack="True">
                    <asp:ListItem Value="0">.NET文章</asp:ListItem>
                    <asp:ListItem Value="1">Java文章</asp:ListItem>
                    <asp:ListItem Value="2">Web开发</asp:ListItem>
                    <asp:ListItem Value="3">数据库</asp:ListItem>
                    <asp:ListItem Value="4">系统综合</asp:ListItem>
                </asp:DropDownList>
            </div>

            <%-- 文章编辑器 --%>
            <div class="arteditor">
                <%--                <CKEditor:CKEditorControl ID="CKEditorControl1" runat="server" BasePath="~/artpublish/ckeditor"
                    Skin="kama" Width="800px" Height="1200px">
                </CKEditor:CKEditorControl>--%>

                <!-- 加载编辑器的容器 -->
                <script id="container" name="content" type="text/plain" style="width: 800px; height: 700px">
                </script>
                <!-- 配置文件 -->
                <script type="text/javascript" src="ueditor/ueditor.config.js"></script>
                <!-- 编辑器源码文件 -->
                <script type="text/javascript" src="ueditor/ueditor.all.js"></script>
                <!-- 实例化编辑器 -->
                <script type="text/javascript">
                    var ue = UE.getEditor('container', { Width: 500 });
                </script>

                <asp:Button ID="btnPublish" runat="server" Text="发布文章" OnClick="btnPublish_Click" />

                <div id="div_text" runat="server">
                </div>
            </div>

        </div>
    </form>
</body>
</html>
