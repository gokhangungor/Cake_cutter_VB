Option Explicit On
Imports System.Math
Imports Emgu.CV
Imports Emgu.CV.CvEnum
Imports Emgu.CV.Structure
Imports Emgu.CV.UI

Public Class Test_NumReg2
    ' Sample VB.NET program to display R[1] and send a new value
    ' to/from any controller
    '
    ' Declarations
    '

    '   Public Class NumRegTest

    Dim capWebcam As Capture
    Dim blnCapturingInProcess As Boolean = False
    Dim imgOriginal As Image(Of Bgr, Byte)
    Dim imgProcessed As Image(Of Gray, Byte)
    Dim screenshot As Bitmap
    Dim xcap As Integer
    Dim ycap As Integer
    Dim z1 As Integer
    Dim z2 As Integer
    Dim noppl As Integer
    Dim radius As Integer
    Dim deltatheta As Double
    Dim alphatheta As Double
    Dim cx, cy, pcam As Double

    Dim xo, yo As Double

    Private mobjRobot As FRRobot.FRCRobot
    Private WithEvents mobjRegs As FRRobot.FRCVars
    Dim Test1 As FRRobot.FRCVars

    Sub New()
        InitializeComponent()
    End Sub

    ' Handle the Connect/Disconnect button click 
    Private Sub cmdConnect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConnect.Click
        outbox.Text = ""
        Try

            If cmdConnect.Text = "Connect" Then

                txtRegValue.Text = String.Format("Connecting to {0} Please wait.", txtHostName.Text)

                mobjRobot = New FRRobot.FRCRobot
                mobjRobot.Connect(txtHostName.Text)
                mobjRegs = mobjRobot.RegNumerics

                '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                'MAIN LOOP'
                '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                'Try
                'capWebcam = New Capture()                           'associate the capture object to the default webcam
                'Catch ex As Exception                                       'catch error if unsuccessful
                'txtXYRadius.Text = ex.Message                   'show error message in text box
                'Return                                                              'and bail
                'End Try

                ''%%%%%%%%%%%%
                'Close Gripper
                If mobjRobot.RegNumerics(2).Value.RegLong = 1 Then
                    mobjRobot.RegNumerics(1).Value.RegLong = 6
                    mobjRobot.RegNumerics(2).Value.RegLong = 0
                    While mobjRobot.RegNumerics(2).Value.RegLong = 0
                        System.Threading.Thread.Sleep(200)
                    End While
                End If
                ''%%%%%%%%%%%%

                radius = rbox.Text
                z2 = zbox.Text
                z1 = -100 + zbox.Text
                pcam = 0.0018
                noppl = ibox.Text
                deltatheta = 2 * Math.PI / noppl

                blnCapturingInProcess = True
                outbox.AppendText("Centre of Cake at (px):" & xcap & "," & ycap & vbCrLf)
                cx = (744 * (ycap - 864) * (pcam / 3.67)) + xbox.Text
                cy = (744 * (xcap - 1152) * (pcam / 3.67)) + ybox.Text
                outbox.AppendText("Centre of Cake at (mm):" & cx & "," & cy & vbCrLf)
                '' move to Ready
                If mobjRobot.RegNumerics(2).value.reglong = 1 Then

                    mobjRobot.RegNumerics(1).value.reglong = 2
                    mobjRobot.RegNumerics(2).value.reglong = 0

                    While mobjRobot.RegNumerics(2).value.reglong = 0
                        System.Threading.Thread.Sleep(200)
                    End While
                End If

                For index As Integer = 1 To (noppl / 2)
                    xo = (radius * Cos((index - 1) * deltatheta))
                    yo = (radius * Sin((index - 1) * deltatheta))
                    alphatheta = PI / 2 + deltatheta * (index - 1)
                    outbox.AppendText("Delta xo:" & xo & ", yo:" & yo & ", alpha:" & alphatheta & vbCrLf)
                    '' MOVE TO JOINT COORDINATES
                    If mobjRobot.RegNumerics(2).Value.RegLong = 1 Then

                        Dim IKxyz(3) As Double
                        IKxyz = invKin((cx - xo), (cy - yo), z2, alphatheta)

                        outbox.AppendText("1. Moving to " & Math.Round(cx - xo) & "," & Math.Round(cy - yo) & "," & z2 & vbCrLf)
                        outbox.AppendText("Joint Angles: " & IKxyz(1) & ", " & IKxyz(2) & ", " & IKxyz(3) & ", " & IKxyz(4) & ", " & IKxyz(5) & vbCrLf)
                        '' theta1, theta2, theta3, theta4, theta5, speed (percent)
                        moveJnt(IKxyz(1), IKxyz(2), IKxyz(3), IKxyz(4), IKxyz(5), 20)
                        ''moveJnt(15, 30, 0, 0, 0, 20)

                        mobjRobot.RegNumerics(1).Value.RegLong = 4
                        mobjRobot.RegNumerics(2).Value.RegLong = 0
                        While mobjRobot.RegNumerics(2).Value.RegLong = 0
                            System.Threading.Thread.Sleep(200)
                        End While
                    End If

                    '' MOVE TO JOINT COORDINATES
                    If mobjRobot.RegNumerics(2).Value.RegLong = 1 Then

                        Dim IKxyz(3) As Double
                        IKxyz = invKin((cx - xo), (cy - yo), z1, alphatheta)
                        outbox.AppendText("2. Moving to " & Math.Round(cx - xo) & "," & Math.Round(cy - yo) & "," & z1 & vbCrLf)
                        outbox.AppendText("Joint Angles: " & IKxyz(1) & ", " & IKxyz(2) & ", " & IKxyz(3) & ", " & IKxyz(4) & ", " & IKxyz(5) & vbCrLf)
                        '' theta1, theta2, theta3, theta4, theta5, speed (percent)
                        moveJnt(IKxyz(1), IKxyz(2), IKxyz(3), IKxyz(4), IKxyz(5), 20)
                        ''moveJnt(15, 30, 0, 0, 0, 20)

                        mobjRobot.RegNumerics(1).Value.RegLong = 4
                        mobjRobot.RegNumerics(2).Value.RegLong = 0
                        While mobjRobot.RegNumerics(2).Value.RegLong = 0
                            System.Threading.Thread.Sleep(200)
                        End While
                    End If

                    If mobjRobot.RegNumerics(2).Value.RegLong = 1 Then

                        Dim IKxyz(3) As Double
                        IKxyz = invKin(cx, cy, z1, alphatheta)
                        outbox.AppendText("3. Moving to " & Math.Round(cx) & "," & Math.Round(cy) & "," & z1 & vbCrLf)
                        outbox.AppendText("Joint Angles: " & IKxyz(1) & ", " & IKxyz(2) & ", " & IKxyz(3) & ", " & IKxyz(4) & ", " & IKxyz(5) & vbCrLf)
                        '' theta1, theta2, theta3, theta4, theta5, speed (percent)
                        moveJnt(IKxyz(1), IKxyz(2), IKxyz(3), IKxyz(4), IKxyz(5), 20)
                        ''moveJnt(15, 30, 0, 0, 0, 20)

                        mobjRobot.RegNumerics(1).Value.RegLong = 4
                        mobjRobot.RegNumerics(2).Value.RegLong = 0
                        While mobjRobot.RegNumerics(2).Value.RegLong = 0
                            System.Threading.Thread.Sleep(200)
                        End While
                    End If

                    '' MOVE TO JOINT COORDINATES
                    If mobjRobot.RegNumerics(2).Value.RegLong = 1 Then

                        Dim IKxyz(3) As Double
                        IKxyz = invKin((cx + xo), (cy + yo), z1, alphatheta)
                        outbox.AppendText("4. Moving to " & Math.Round(cx + xo) & "," & Math.Round(cy + yo) & "," & z1 & vbCrLf)
                        outbox.AppendText("Joint Angles: " & IKxyz(1) & ", " & IKxyz(2) & ", " & IKxyz(3) & ", " & IKxyz(4) & ", " & IKxyz(5) & vbCrLf)
                        '' theta1, theta2, theta3, theta4, theta5, speed (percent)
                        moveJnt(IKxyz(1), IKxyz(2), IKxyz(3), IKxyz(4), IKxyz(5), 20)
                        ''moveJnt(15, 30, 0, 0, 0, 20)

                        mobjRobot.RegNumerics(1).Value.RegLong = 4
                        mobjRobot.RegNumerics(2).Value.RegLong = 0
                        While mobjRobot.RegNumerics(2).Value.RegLong = 0
                            System.Threading.Thread.Sleep(200)
                        End While
                    End If

                    '' MOVE TO JOINT COORDINATES
                    If mobjRobot.RegNumerics(2).Value.RegLong = 1 Then

                        Dim IKxyz(3) As Double
                        IKxyz = invKin((cx + xo), (cy + yo), z2, alphatheta)
                        outbox.AppendText("5. Moving to " & Math.Round(cx + xo) & "," & Math.Round(cy + yo) & "," & z2 & vbCrLf)
                        outbox.AppendText("Joint Angles: " & IKxyz(1) & ", " & IKxyz(2) & ", " & IKxyz(3) & ", " & IKxyz(4) & ", " & IKxyz(5) & vbCrLf)
                        '' theta1, theta2, theta3, theta4, theta5, speed (percent)
                        moveJnt(IKxyz(1), IKxyz(2), IKxyz(3), IKxyz(4), IKxyz(5), 20)
                        ''moveJnt(15, 30, 0, 0, 0, 20)

                        mobjRobot.RegNumerics(1).Value.RegLong = 4
                        mobjRobot.RegNumerics(2).Value.RegLong = 0
                        While mobjRobot.RegNumerics(2).Value.RegLong = 0
                            System.Threading.Thread.Sleep(200)
                        End While
                    End If
                Next



                ''%%%%%%%%%%%%
                ''Open Gripper
                'If mobjRobot.RegNumerics(2).Value.RegLong = 1 Then
                '    mobjRobot.RegNumerics(1).Value.RegLong = 5
                '    mobjRobot.RegNumerics(2).Value.RegLong = 0
                '    While mobjRobot.RegNumerics(2).Value.RegLong = 0
                '        System.Threading.Thread.Sleep(200)
                '    End While
                'End If
                ''%%%%%%%%%%%%



                ''%%%%%%%%%%%%
                ''Close Gripper
                'If mobjRobot.RegNumerics(2).Value.RegLong = 1 Then
                ''   mobjRobot.RegNumerics(1).Value.RegLong = 6
                ''  mobjRobot.RegNumerics(2).Value.RegLong = 0
                '' While mobjRobot.RegNumerics(2).Value.RegLong = 0
                ''    System.Threading.Thread.Sleep(200)
                'End While
                'End If
                ''%%%%%%%%%%%%



                '' move to Home
                If mobjRobot.RegNumerics(2).value.reglong = 1 Then

                    mobjRobot.RegNumerics(1).value.reglong = 1
                    mobjRobot.RegNumerics(2).value.reglong = 0

                    While mobjRobot.RegNumerics(2).value.reglong = 0
                        System.Threading.Thread.Sleep(200)
                    End While
                End If


                '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                ''End the motion by setting R(1) to 99
                mobjRobot.RegNumerics(1).Value.RegLong = 99
                mobjRobot.RegNumerics(2).Value.RegLong = 0
                System.Threading.Thread.Sleep(2000)

                ''Zero the Register Values before disconnecting!
                zeroRegs()

            Else    ' must be the user wants to disconnect

                txtRegValue.Text = "Releasing the Robot objects"
                ReleaseObjects()
                txtRegValue.Text = "Not Connected"
            End If

        Catch ex As System.Runtime.InteropServices.COMException
            ' The only time an error is expected is during connect
            MsgBox(String.Format("{0} - {1}", ex.ErrorCode, ex.Message))
            ReleaseObjects()
        Catch ex As Exception
            MsgBox(ex.Message)
            ReleaseObjects()
        End Try

        If mobjRobot IsNot Nothing AndAlso mobjRobot.IsConnected Then
            cmdConnect.Text = "Disconnect"
        Else
            cmdConnect.Text = "Connect"
        End If

    End Sub

    Sub ProcessFrameAndUpdateGUI(sender As Object, arg As EventArgs)
        'imgOriginal = capWebcam.QueryFrame()                                'get next frame from the webcam
        imgOriginal = New Emgu.CV.Image(Of Bgr, Byte)(screenshot)                                'get next frame from the webcam
        If (imgOriginal Is Nothing) Then                                                  'if we did not get a frame
            Return                                                                                      'bail
        End If
        'min filter value (if color is greater than or equal to this)
        'max filter value (if color is less than or equal to this)
        imgProcessed = imgOriginal.InRange(New Bgr(0, 0, 175), New Bgr(100, 100, 256))
        'imgProcessed = imgOriginal.InRange(New Bgr(219, 142, 222), New Bgr(244, 227, 245))

        imgProcessed = imgProcessed.SmoothGaussian(9)                   'we call SmoothGaussian with only one param, the x and y size of the filter window

        'Canny threshold
        'accumulator threshold
        'size of image / this param = "accumulator resolution"
        'min distance in pixels between the centers of the detected circles
        'min radius of detected circle
        'max radius of detected circle
        'get circles from the first channel
        Dim circles As CircleF() = imgProcessed.HoughCircles(New Gray(100), New Gray(50), 2, imgProcessed.Height / 4, 10, 400)(0)

        For Each CircleF In circles
            'x position of center point of circle
            'y position of center point of circle
            'radius of circle
            'txtXYRadius.AppendText("ball position x =" + CircleF.Center.X.ToString().PadLeft(4) + ", y =" + CircleF.Center.Y.ToString().PadLeft(4) + ", radius =" + CircleF.Radius.ToString("###.000").PadLeft(7))

            xcap = CInt(CircleF.Center.X)
            ycap = CInt(CircleF.Center.Y)

            'Draw a small green circle at the center of the detected object. To do this, we will call the OpenCV 1.x function, this is necessary
            'b/c we are drawing a circle of radius 3, even though the size of the detected circle will be much bigger.
            'The CvInvoke object can be used to make OpenCV 1.x function calls
            'draw on the original image
            'center point of circle
            'radius of circle in pixels
            'draw pure green
            'thickness of circle in pixels, -1 indicates to fill the circle
            'use AA to smooth the pixels
            'no shift
            CvInvoke.cvCircle(imgOriginal, New Point(CInt(CircleF.Center.X), CInt(CircleF.Center.Y)), 3, New MCvScalar(0, 255, 0), -1, LINE_TYPE.CV_AA, 0)

            'draw a red circle around the detected object
            'current circle we are on in For Each loop
            'draw pure red
            'thickness of circle in pixles
            imgOriginal.Draw(CircleF, New Bgr(Color.Red), 3)
        Next

        'ibOriginal.Image = imgOriginal
        'ibProcessed.Image = imgProcessed

    End Sub





    '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

    '' FORWARD KINEMATICS
    '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

    Public Function invKin(ByVal x As Double, ByVal y As Double, ByVal z As Double, ByVal alpha As Double)

        '' Define pi:
        Dim pi, link2, link3, th1, th2, th3, th1f, th2f, th3f, th4f, th5f As Double
        pi = 22 / 7
        link2 = 260
        link3 = 290

        '' DH to Fanuc Conversion:
        Dim s, r, D As Double
        s = z
        r = Sqrt(Pow(x, 2) + Pow(y, 2))
        D = (Pow(s, 2) + Pow(r, 2) - Pow(link2, 2) - Pow(link3, 2)) / (2 * link2 * link3)

        th1 = Atan2(y, x)
        th3 = Atan2(-Sqrt(Abs(1 - Pow(D, 2))), D)
        th2 = Atan2(s, r) - Atan2(link3 * Sin(th3), link2 + link3 * Cos(th3))

        th1f = th1
        th2f = (pi / 2) - th2
        th3f = th2 + th3 - (4 * pi / 180)
        th4f = -(pi / 2) - th3f
        th5f = alpha

        '' Radian to Degree Conversion:
        th1f = th1f * 180 / pi
        th2f = th2f * 180 / pi
        th3f = th3f * 180 / pi
        th4f = th4f * 180 / pi
        th5f = th5f * 180 / pi

        Dim outIK(5) As Double
        outIK(1) = th1f
        outIK(2) = th2f
        outIK(3) = th3f
        outIK(4) = th4f
        outIK(5) = th5f

        Return outIK

    End Function






    '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

    '' MOTION FUNCTIONS
    '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

    '' LINEAR MOTION FUNCTION
    Private Sub moveLin(xLin As Double, yLin As Double, zLin As Double, wLin As Double, pLin As Double, rLin As Double, speedLin As Double)
        '' POINT1
        mobjRobot.RegNumerics(11).Value.RegLong = xLin     '' Point1 x-axis
        mobjRobot.RegNumerics(12).Value.RegLong = yLin     '' Point1 y-axis
        mobjRobot.RegNumerics(13).Value.RegLong = zLin     '' Point1 z-axis
        mobjRobot.RegNumerics(14).Value.RegLong = wLin     '' Point1 w-aangle
        mobjRobot.RegNumerics(15).Value.RegLong = pLin     '' Point1 p-angle
        mobjRobot.RegNumerics(16).Value.RegLong = rLin     '' Point1 r-angle
        '%%%%%%%%
        mobjRobot.RegNumerics(10).Value.RegLong = speedLin

    End Sub


    '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    '' JOINT MOTION FUNCTION
    Private Sub moveJnt(theta1 As Double, theta2 As Double, theta3 As Double, theta4 As Double, theta5 As Double, speedJnt As Double)
        '' POINT1
        mobjRobot.RegNumerics(21).Value.RegLong = theta1     '' Point1 x-axis
        mobjRobot.RegNumerics(22).Value.RegLong = theta2     '' Point1 y-axis
        mobjRobot.RegNumerics(23).Value.RegLong = theta3     '' Point1 z-axis
        mobjRobot.RegNumerics(24).Value.RegLong = theta4     '' Point1 w-aangle
        mobjRobot.RegNumerics(25).Value.RegLong = theta5     '' Point1 p-angle
        '%%%%%%%%
        mobjRobot.RegNumerics(20).Value.RegLong = speedJnt

    End Sub



    '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    '' Zero the Register Values
    Private Sub zeroRegs()
        Dim counterZR As Integer = 1
        For counterZR = 1 To 200
            mobjRobot.RegNumerics(counterZR).Value.RegLong = 0
        Next counterZR
    End Sub



    '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


    ' Fully releasing COM objects requires waiting for garbage collection
    Private Sub ReleaseObjects()
        mobjRegs = ReleaseObject("mobjRegs", mobjRegs)
        mobjRobot = ReleaseObject("mobjRobot", mobjRobot)
        System.GC.Collect()
    End Sub

    ' Wrap object release in Try-Catch for enhanced diagnostics
    Private Function ReleaseObject(ByVal identifier As String, ByRef item As Object) As Object
        Try
            item = Nothing
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(String.Format("Error releasing {0}.{1}Error: {2}", identifier, Environment.NewLine, ex.Message))
        End Try

        Return Nothing
    End Function



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '
        mobjRobot.RegNumerics(1).Value.RegLong = txtRegValue.Text
    End Sub

    Private Sub Test_NumReg2_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim foundimage As String
            foundimage = AppDomain.CurrentDomain.BaseDirectory & "Cake.PNG"
            screenshot = CType(Image.FromFile(foundimage, True), Bitmap)
            picbox.Image = screenshot
        Catch ex As Exception

        End Try

        AddHandler Application.Idle, New EventHandler(AddressOf Me.ProcessFrameAndUpdateGUI)        'add process image function to the application's list of tasks
    End Sub

End Class

