<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="Fisharoo.FisharooWeb.Friends.test" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Default</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="VBScript">
			Function OpenABW()
			
				'define local variables used in this method
				Dim objAddrBkWrap
				Dim objContacts
				Dim objABWOp
				Dim objContact
				
				'Initialize variables for the messages displayed to the user during the contact importing process
				const L_NeedSharePointAddrBook_Text = "To import contacts, you must have an Address Book compatible with Windows SharePoint Services and you must have Internet Explorer 5.0 or greater."
				const L_SelectUsers_Text = "Select Users to Import"
				const L_Add_Text = "Add"
				
				'Initialize the return value to an be an emptystring
				OpenABW = ""
				
				'Turn on our simple error handling (which is essentially disabling errors from being raised to the browser)
				On Error Resume Next

				'Create an instance of the Sharepoint Address Book Import component, which is defined in Msosvabw.dll			    
				Set objAddrBkWrap = CreateObject("MsSvAbw.AddrBookWrapper")			    


				If not IsObject(objAddrBkWrap) then
					
					'We don't have a valid object, so tell the user the import couldn't be performed
					MsgBox L_NeedSharePointAddrBook_Text
					OpenABW = ""

				Else
					
					'Using the new Sharepoint Address Book Import object, prompt the user to select some entries (Contacts)
					'...which are returned in the objContacts collection variable
					'objABWOp = objAddrBkWrap.AddressBook(, 1, , , , objContacts, , , True)
					objABWOp = objAddrBkWrap.AddressBook(L_SelectUsers_Text&"", 1, L_Add_Text&"", "", "", objContacts, Nothing, Nothing, True)
					
					'Display the list of selected Address Book Entries - for debugging purposes					
					'call window.alert("objContacts.count: " + cstr(objContacts.count))
					
					'if we have a valid return value from the previous method, then process the selections
					If objABWOp <> 0 then
						OpenABW = ""
					Else
						'Process the selected Address Book Entries by iterating through them and building an XML document
						'...containing all of the selected Contacts and their information.  
						OpenABW = ProcessABWCollection(objContacts)
					End If
					    
				End If

				'turn off our "On Error Resume Next" error handling
				On Error GoTo 0
			End Function


			Function EnsureImport()
				'This method creates an instance of the SharePoint.SpreadsheetLauncher component to ensure the 
				'...presence and availability of the component

				Dim objEnsureImport

				'Initialize the return value to false (0)			      
				EnsureImport = 0
					
				'Turn on our simple error handling (which is essentially disabling errors from being raised to the browser)
				On Error Resume Next
					
				'The Sample in the SDK indicates that you should use the ProgID "SharePoint.SpreadsheetLauncher.1"
				'...however, WSS clearly uses the ProgID without specifying the version number.
				'Set objEnsureImport = CreateObject("SharePoint.SpreadsheetLauncher.1")
				Set objEnsureImport = CreateObject("SharePoint.SpreadsheetLauncher")

				'Return whether or not we can do the import			    
				if not IsObject(objEnsureImport) then
					EnsureImport = -1
				else
					objEnsureImport.EnsureImport()
				end if
					
				'turn off our "On Error Resume Next" error handling
				On Error GoTo 0
			End Function
		</SCRIPT>
		<SCRIPT language="JavaScript">   
					                                
			function btnSelectContacts_Click()
			{
				ImportFromAddressBook();
			}

			function ImportFromAddressBook()	
			{
				var strXMLContacts = "";
			
				//Verify that the Sharepoint Address Book (Contact) Import components are installed and available
				if (EnsureImport() == 0)	
				{
					//Now prompt the user to select Address Book Entries and conver their selections into an XML structure
					strXMLContacts = DoImportFromAddressBook();
						
					if (strXMLContacts.length > 0)	        
					{  
						//Convert the < and > to their equivalent HTML encoded characters, so we 
						//...can pass the ASP.NET page request validation.
						strXMLContacts = strXMLContacts.replace(/</g, "&lt;");
						strXMLContacts = strXMLContacts.replace(/>/g, "&gt;");
				
						//Put the XML data for the selected contacts into the hidden HTML input element
						Form1.xmlContacts.value = strXMLContacts;
						
						//Auto-submit the form to send the XML data to the server
						Form1.submit();
					}
				}
			}
			
			function DoImportFromAddressBook()	
			{
				//Simple call the OpenABW (ABW - Address Book Wrapper) and return the XML data string containing all of 
				//...the data for the selected Address Book Entries (contacts)		
				return OpenABW();			
			}

				
			function ProcessABWCollection(col)	
			{
				try 
				{
					
					//Define a string array containing a list of all of the Property names for working with a Contact object
					var rgstProps  = new Array("FirstName", "LastName", "SMTPAddress", "CompanyName", "JobTitle", "HomeTelephoneNumber", 
						"BusinessTelephoneNumber", "MobileTelephoneNumber", "BusinessFaxNumber", "BusinessAddressStreet", 
						"BusinessAddressCity", "BusinessAddressState", "BusinessAddressPostalCode", "BusinessAddressCountry", "Body");
						
					//Also, define a string array containing a list of all the Field names for a contact
					//NOTE:  These could be different values, but the positions of items in this array need to match up 
					//...with the Properties array
					var rgstFields = new Array("FirstName", "Title", "Email", "Company", "JobTitle", "HomePhone", "WorkPhone", "CellPhone", 
						"WorkFax", "WorkAddress", "WorkCity", "WorkState", "WorkZip", "WorkCountry", "Comments");

					//define the member variables for this method
					var st;
					var strFieldName;
					var strPropertyName;
					var objContactItem;
					var xmldocContacts;
							
					//Get a new Enumerator object for working with the passed collection of Address Book Entries (contacts)		
					var e = new Enumerator(col);

					//If there were no items selected, then simply return
					if (e.atEnd())
					{			
						return "";
					}
					
					//Create and initialize a new instance of the MSXML2.DOMDocument component, 
					//...which provides us the ability to build an XML document in memory on the fly
					xmldocContacts = new ActiveXObject('Msxml2.DOMDocument');
					xmldocContacts.async = false;
					
					//Create and append the Contacts root element
					var elmContacts = xmldocContacts.createElement("Contacts");
					xmldocContacts.appendChild(elmContacts);		
								
					
					for (; !e.atEnd(); e.moveNext())
					{
						//Create a new Contact element and append it to the <Contacts> root element
						var elmContact = xmldocContacts.createElement("Contact");	
						elmContacts.appendChild(elmContact);		
						
						//Get the current Address Book (contact) item into a variable
						objContactItem = e.item();
						
						//Can access properties of the Contact, like...
						//objContactItem.FirstName;			
						
						//Iterate through each property for a Address Book Entry (contact) in our array and add the Property & value
						//...to our XML object structure.
						for (intPropertyCounter = 0; intPropertyCounter < rgstProps.length; intPropertyCounter++)
						{
							//Get the Field & Property name for the current Property location from the arrays into variables.
							strFieldName = rgstFields[intPropertyCounter];
							strPropertyName = rgstProps[intPropertyCounter];
						
							//Create a new Element for the current contact field (based on our array of fields, initialized earlier)
							//...and append it ot the <Contact> element
							var elmContactField = xmldocContacts.createElement(strFieldName);
							elmContact.appendChild(elmContactField);
							
							//Assign the value for the current contact property to the new Contact field's element
							elmContactField.text = objContactItem[strPropertyName];
						}
					}
					
					//Return the XML data for the selected Address Book Entries (Contacts)
					return xmldocContacts.xml;				
				}		
				catch (excp) 
				{
					window.alert("An error occured processing the list of selected Address Book Entries (contacts).");
					return null;
				}		
			}
		</SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:TextBox id="TextBox1" style="Z-INDEX: 101; LEFT: 24px; POSITION: absolute; TOP: 72px" runat="server"
				TextMode="MultiLine" Width="536px" Height="120px"></asp:TextBox>
			<INPUT id="btnSelectContacts" style="Z-INDEX: 102; LEFT: 24px; POSITION: absolute; TOP: 16px"
				onclick="javascript:return btnSelectContacts_Click();" type="button" value="Select Contacts">
			<INPUT ID="xmlContacts" runat="server" type="hidden" name="xmlContacts" style="Z-INDEX: 103; LEFT: 288px; POSITION: absolute; TOP: 24px">
		</form>
	</body>
</HTML>

