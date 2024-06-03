<%@ Import Namespace="Fisharoo.FisharooCore.Core.Domain"%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Ratings.ascx.cs" Inherits="Fisharoo.FisharooWeb.UserControls.Ratings" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Panel ID="pnlRating" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:LinkButton ID="lbRateThis" runat="server" Text="rate this!" OnClick="lbRateThis_Click"></asp:LinkButton>
            <asp:Label ID="lblThankYou" runat="server" Text="Thank you!" Visible="false"></asp:Label>
            <cc1:Rating ID="Rating1" Enabled="false" ReadOnly="true" 
                runat="server" 
                MaxRating="5" 
                EmptyStarCssClass="ratingStarEmpty" 
                FilledStarCssClass="ratingStarFilled" 
                StarCssClass="ratingStar" 
                WaitingStarCssClass="ratingStarSaved">
                </cc1:Rating>
        
            <asp:Panel ID="pnlModalPopup" runat="server" BackColor="White" ScrollBars="Vertical">
                <asp:Literal ID="litSelectedRatings" Visible="true" runat="server"></asp:Literal>
                <asp:Repeater ID="repRatingOptions" runat="server">
                    <HeaderTemplate>
                        <table>    
                            <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                            <tr>
                                <td>
                                    <cc1:Rating id="Rating1" runat="server" 
                                        MaxRating="5" 
                                        EmptyStarCssClass="ratingStarEmpty" 
                                        FilledStarCssClass="ratingStarFilled" 
                                        StarCssClass="ratingStar" 
                                        WaitingStarCssClass="ratingStarSaved" 
                                        OnChanged="rating_Changed" 
                                        Tag='<%# ((SystemObjectRatingOption)Container.DataItem).SystemObjectRatingOptionID.ToString() %>'>
                                        </cc1:Rating>
                                </td>
                                <td>
                                    <asp:Label ID="lblOptionName" 
                                        ToolTip='<%# ((SystemObjectRatingOption)Container.DataItem).Description %>' 
                                        Text='<%# ((SystemObjectRatingOption)Container.DataItem).Name %>' 
                                        runat="server"></asp:Label>
                                </td>
                            </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Button ID="btnSave" UseSubmitBehavior="false" OnClick="btnSave_Click" runat="server" Text="Save" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
            </asp:Panel>
            
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" 
                runat="server" 
                TargetControlID="lbRateThis" 
                PopupControlID="pnlModalPopup" 
                DropShadow="true" 
                OkControlID="btnSave" 
                CancelControlID="btnCancel"></cc1:ModalPopupExtender>
                </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>