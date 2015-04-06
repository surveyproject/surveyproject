<%@ Page Language="C#" CodeBehind="ColumnChartReport.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.ColumnChartReport" %>


						<asp:CHART id="Chart1" runat="server" ImageLocation="~/Images/mscharts_temp/ChartPic_#SEQ(300,3)"  ImageType="Png"  RenderType="BinaryStreaming"
                            Palette="Excel" BackColor="white" Width="512px" Height="396px" BorderlineDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1">
							<titles>
								<asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="SP Column Chart" Name="SP Title1" ForeColor="26, 59, 105"></asp:Title>
							</titles>
							<legends>
								<asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Enabled="True" Name="Default">
                                    <CustomItems>
										<asp:LegendItem Name="LegendItem" Color="Red">
											<Cells>
												<asp:LegendCell Text="Central" Alignment="MiddleLeft" Name="Cell1">
													<Margins Left="15" Right="15"></Margins>
												</asp:LegendCell>
												<asp:LegendCell Text="2480.18" Alignment="MiddleRight" Name="Cell2">
													<Margins Left="15" Right="15"></Margins>
												</asp:LegendCell>
												<asp:LegendCell Text="69.35" ForeColor="LimeGreen" Alignment="MiddleRight" Name="Cell3">
													<Margins Left="15" Right="15"></Margins>
												</asp:LegendCell>
												<asp:LegendCell CellType="Image" Alignment="MiddleLeft" Name="Cell4">
													<Margins Left="15" Right="15"></Margins>
												</asp:LegendCell>
												<asp:LegendCell Text="2.88%" ForeColor="LimeGreen" Alignment="MiddleLeft" Name="Cell5">
													<Margins Left="15" Right="15"></Margins>
												</asp:LegendCell>
											</Cells>
										</asp:LegendItem>
									</CustomItems>
								</asp:Legend>
							</legends>
							<borderskin SkinStyle="Emboss"></borderskin>
							<series>
								<asp:Series XValueType="Double" YValueType="Double" Name="Series1" BorderColor="180, 26, 59, 105">
								</asp:Series>
							</series>
							<chartareas>
								<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
									<area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
									<axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"  Format="" />
										<MajorGrid LineColor="64, 64, 64, 64" />
									</axisy>
									<axisx LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" IsEndLabelVisible="False" Format="" />
										<MajorGrid LineColor="64, 64, 64, 64" />
									</axisx>
								</asp:ChartArea>
							</chartareas>
						</asp:CHART>
   
