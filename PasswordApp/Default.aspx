<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PasswordApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div class="header">
            <h3>Passwords:</h3>
        </div>
        <div class="gvPasswords">
            <asp:GridView ID="passwordGV" runat="server" OnRowEditing="passwordGV_RowEditing" OnRowCancelingEdit="passwordGV_RowCancelingEdit"
                OnRowUpdating="passwordGV_RowUpdating" OnRowDeleting="passwordGV_RowDeleting" >
                <Columns>
                    <asp:BoundField DataField="Account" HeaderText="Account"/>
                    <asp:BoundField DataField="Password" HeaderText="Password"/>
                    <asp:CommandField ShowEditButton="true" />
                </Columns>
            </asp:GridView>
        </div>
    </main>

</asp:Content>
