<%@ Page Title="" Language="C#" MasterPageFile="~/User/MasterPage.master" AutoEventWireup="true" CodeFile="artist.aspx.cs" Inherits="User_artist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style="margin-top:25px; border-bottom:50px">
        <asp:DataList ID="DataListBrows" runat="server" RepeatColumns="6">
            <ItemTemplate>
                <table style="margin-left:12px">
                    <tr>
                        <td>
                        <a href="view.aspx?sing=<%# Eval("singer") %>">    <%# Eval("singer") %> </a> 
                        </td>
                        <td>
                            
                        </td>

                    </tr>
                </table>
                <hr />
            </ItemTemplate>
        </asp:DataList>
    </div>

</asp:Content>

