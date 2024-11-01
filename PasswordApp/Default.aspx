<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PasswordApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Menu ID="TabMenu" runat="server" Orientation="Horizontal" CssClass="tab-menu"
              StaticMenuStyle-CssClass="tab-menu" StaticMenuItemStyle-CssClass="tab-option"
              OnMenuItemClick="TabMenu_MenuItemClick">
        <Items>
            <asp:MenuItem Text="Passwords" Value="0"></asp:MenuItem>
            <asp:MenuItem Text="Create a new Password Record" Value="1"></asp:MenuItem>
        </Items>
    </asp:Menu>
    
    <div class="main-container">
        <asp:MultiView runat="server" ID="TabView" ActiveViewIndex="0">
            <asp:View ID="PasswordsView" runat="server">
                <div class="header">
                    <h3>Passwords</h3>
                </div>
                <div class="grid-container">
                    <asp:GridView ID="passwordGV" runat="server" CssClass="grid-view"
                                  OnRowEditing="passwordGV_RowEditing" OnRowCancelingEdit="passwordGV_RowCancelingEdit"
                                  OnRowUpdating="passwordGV_RowUpdating" OnRowDeleting="passwordGV_RowDeleting" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="Site" HeaderText="Site" />
                            <asp:BoundField DataField="EmailorUsername" HeaderText="Username/Email" />
                            <asp:BoundField DataField="Password" HeaderText="Password" />
                            <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="save-section">
                    <asp:Button ID="SaveButton" runat="server" Text="Save Changes" CssClass="save-button" OnClick="SaveButton_Click" />
                </div>
            </asp:View>

            <asp:View ID="CreatePasswordView" runat="server">
                <div class="form-container">
                    <asp:TextBox ID="txtNewSite" runat="server" Placeholder="Site" CssClass="input-field"></asp:TextBox>
                    <asp:TextBox ID="txtNewUsername" runat="server" Placeholder="Username/Email" CssClass="input-field"></asp:TextBox>
                    <asp:TextBox ID="txtNewPassword" runat="server" Placeholder="Password" TextMode="Password" CssClass="input-field"></asp:TextBox>
                    <asp:Button ID="btnAddNew" runat="server" Text="Add New Entry" CssClass="add-button" OnClick="btnAddNew_Click" />
                </div>
            </asp:View>
        </asp:MultiView>
    </div>

    <style>
        /* General container styling */
        .main-container {
            max-width: 800px;
            margin: 30px auto;
            padding: 20px;
            background-color: #f9f9f9;
            box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
        }

        /* Tab menu styling */
        .tab {
            width: 400px;
            overflow: hidden;
            background-color: #f1f1f1;
        }

        .tab-option {
            margin-left: 20px;
            background-color: inherit;
            float: left;
            border: none;
            outline: none;
            cursor: pointer;
            padding: 14px 16px;
            transition: 0.3s;
            color: black;
        }

        .tab-option:hover {
            background-color: #ddd;
        }

        .tab-option.active {
            background-color: #ccc;
        }

        /* Header styling */
        .header h3 {
            font-size: 22px;
            margin-bottom: 20px;
        }

        /* GridView styling */
        .grid-container {
            overflow-x: auto;
        }

        .grid-view {
            width: 100%;
            border: none;
            font-size: 15px;
            border-collapse: collapse;
            background-color: #ffffff;
        }

        .grid-view th {
            background-color: #6f9cff;
            /*color: #ffffff;*/
            padding: 12px;
            text-align: left;
        }

        .grid-view td {
            padding: 12px;
            border-bottom: 1px solid #ddd;
        }

        .grid-view tr:hover {
            background-color: #f1f1f1;
        }

        /* Button styling */
        .save-button, .add-button {
            display: inline-block;
            margin-top: 20px;
            padding: 10px 20px;
            font-size: 16px;
            background-color: #447fff;
            color: #ffffff;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .save-button:hover, .add-button:hover {
            background-color: #53607e;
        }

        /* Input field styling */
        .input-field {
            display: block;
            width: calc(100% - 22px);
            padding: 10px;
            margin: 10px 0;
            font-size: 16px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        /* Section styling */
        .save-section {
            text-align: center;
        }

        .form-container {
            display: flex;
            flex-direction: column;
            align-items: center;
        }
    </style>
</asp:Content>
