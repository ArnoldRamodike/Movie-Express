﻿' Generated 22 Dec 2014 13:51 - Singular Systems Object Generator Version 2.1.661
Imports Csla
Imports Csla.Serialization
Imports Csla.Data
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
#If SILVERLIGHT = False Then
Imports System.Data.SqlClient
#End If

Namespace Reporting.Dynamic

  <Serializable()> _
  Public Class ReportList
    Inherits SingularBusinessListBase(Of ReportList, Report)

#Region "  Parent  "

    <NotUndoable()> Private mParent As ReportGroup
#End Region

#Region "  Business Methods  "

    Public Function GetItem(DynamicReportID As Integer) As Report

      For Each child As Report In Me
        If child.DynamicReportID = DynamicReportID Then
          Return child
        End If
      Next
      Return Nothing

    End Function

    Public Overrides Function ToString() As String

      Return "Dynamic Reports"

    End Function

    Public Function GetReportParameter(DynamicReportParameterID As Integer) As ReportParameter

      Dim obj As ReportParameter = Nothing
      For Each parent As Report In Me
        obj = parent.ReportParameterList.GetItem(DynamicReportParameterID)
        If obj IsNot Nothing Then
          Return obj
        End If
      Next
      Return Nothing

    End Function

#End Region

#Region "  Data Access  "

#Region "  Common  "

    Public Shared Function NewReportList() As ReportList

      Return New ReportList()

    End Function

    Public Sub New()

      ' must have parameter-less constructor

    End Sub

#End Region

#Region "  Silverlight  "

#If SILVERLIGHT Then

#End Region

#Region " .NET Data Access "

#Else

#End Region

#Region "  .Net Data Access  "

    <Serializable(), Singular.Web.WebFetchable()> _
    Public Class Criteria
      Inherits CriteriaBase(Of Criteria)

      Public Property ObjectName As String
      Public Property ReportID As Integer

      Public Sub New()


      End Sub

    End Class

    Private Sub Fetch(sdr As SafeDataReader)

      Me.RaiseListChangedEvents = False
      While sdr.Read
        Add(Report.GetReport(sdr))
      End While
      Me.RaiseListChangedEvents = True

      Dim parentChild As Report = Nothing
      If sdr.NextResult() Then
        While sdr.Read
          If parentChild Is Nothing OrElse parentChild.DynamicReportID <> sdr.GetInt32(1) Then
            parentChild = Me.GetItem(sdr.GetInt32(1))
          End If
          parentChild.ReportParameterList.RaiseListChangedEvents = False
          parentChild.ReportParameterList.Add(ReportParameter.GetReportParameter(sdr, parentChild.AutoGeneratedInd))
          parentChild.ReportParameterList.RaiseListChangedEvents = True

        End While
      End If

    End Sub

    Protected Overrides Sub DataPortal_Fetch(criteria As Object)

      Dim crit As Criteria = criteria
      Using cn As New SqlConnection(Singular.Settings.ConnectionString)
        cn.Open()
        Try
          Using cm As SqlCommand = cn.CreateCommand
            cm.CommandType = CommandType.StoredProcedure
            cm.CommandText = "GetProcs.getDynamicReportList"
            cm.Parameters.AddWithValue("@AutoSchema", Settings.DynamicReportsAutoSchema)
            cm.Parameters.AddWithValue("@IncludeParameters", True)
            If crit.ReportID <= 0 Then
              If crit.ObjectName IsNot Nothing Then
                cm.Parameters.AddWithValue("@ObjectName", crit.ObjectName.Replace("[", "").Replace("]", ""))
              End If
            Else
              cm.Parameters.AddWithValue("@ReportID", crit.ReportID)
            End If

            Using sdr As New SafeDataReader(cm.ExecuteReader)
              Fetch(sdr)
            End Using
          End Using
        Finally
          cn.Close()
        End Try
      End Using

    End Sub

    Protected Overrides Sub DataPortal_Update()
      UpdateTransactional(AddressOf Update)
    End Sub

    Friend Sub Update()

      Me.RaiseListChangedEvents = False
      Try
        ' Loop through each deleted child object and call its Update() method
        For Each Child As Report In DeletedList
          Child.DeleteSelf()
        Next

        ' Then clear the list of deleted objects because they are truly gone now.
        DeletedList.Clear()

        ' Loop through each non-deleted child object and call its Update() method
        For Each Child As Report In Me
          If Child.IsNew Then
            Child.Insert()
          Else
            Child.Update()
          End If
        Next
      Finally
        Me.RaiseListChangedEvents = True
      End Try

    End Sub

#End If

#End Region

#End Region

  End Class

End Namespace