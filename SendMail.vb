﻿Imports System
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Threading
Imports System.ComponentModel

Public Class KripaOpenSourceSentMail
    Dim oSmtp As New SmtpClient
    ReadOnly oSmtpPermission As New SmtpPermission(SmtpAccess.Connect)

    Public Function SendMail(oHost As String, oPort As Integer, oSSL As Boolean,
            oUser As String, oPass As String, oFrom As String, oTO() As String,
            oCC() As String, oBCC() As String, oSub As String, oBody As String,
            Fname() As String)
        Try
            oSmtp.Host = oHost
            oSmtp.Credentials = New NetworkCredential(oUser, oPass)
            oSmtp.Port = oPort
            oSmtp.EnableSsl = True
            Dim oSmtpMessage As New MailMessage With {
                .From = New MailAddress(oFrom),
                .Subject = oSub,
                .Body = oBody
            }

            'CC Addresses
            If oCC IsNot Nothing Then
                Dim c As Integer
                For c = 0 To oTO.Count - 1
                    oSmtpMessage.CC.Add(New MailAddress(oCC(c)))
                Next
            End If

            'BCC Addresses
            If oBCC IsNot Nothing Then
                Dim b As Integer
                For b = 0 To oTO.Count - 1
                    oSmtpMessage.Bcc.Add(New MailAddress(oBCC(b)))
                Next
            End If

            'To Addresses
            If oTO Is Nothing Then
                MsgBox("Please Enter an Address to Send")
            Else
                Dim t As Integer
                For t = 0 To oTO.Count - 1
                    oSmtpMessage.To.Add(New MailAddress(oTO(t)))
                Next
            End If

            'Attachments
            If Fname IsNot Nothing Then
                Dim I As Integer
                For I = 0 To Fname.Count - 1
                    oSmtpMessage.Attachments.Add(New Attachment(Fname(I)))
                Next
            End If



            oSmtp.Send(oSmtpMessage)
            MsgBox("Mail Send")
        Catch ex As Exception
            MsgBox("Mail Failed to send")
            Return 1
        End Try
        Return 0
    End Function
End Class

