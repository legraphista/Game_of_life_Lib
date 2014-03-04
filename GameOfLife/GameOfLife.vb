Public Class GameOfLife

#Region "Public variable"
    Public ReadOnly InvalidMatrix As New Exception("The width or height of the matrix does not equal the original matrix")
    Public ReadOnly Property ExpandMatrix As Boolean
        Get
            Return _expMatrix
        End Get
    End Property
    Public ReadOnly Property getWidth
        Get
            Return _w
        End Get
    End Property
    Public ReadOnly Property getHeight
        Get
            Return _h
        End Get
    End Property
    Public ReadOnly Property getMatrix As Boolean()()
        Get
            Return matrix
        End Get
    End Property
#End Region

#Region "Private variables"
    Private _h, _w As Integer
    Private Const _maxMatrixSize As Integer = 4096

    Private _expMatrix As Boolean = False


    Private matrix()() As Boolean
#End Region

#Region "Constructors"
    Sub New(width As Integer, height As Integer)
        ReDim matrix(height - 1)
        For i As Integer = 0 To height - 1
            ReDim matrix(i)(width - 1)
        Next

        For i As Integer = 0 To height - 1
            For j As Integer = 0 To width - 1
                matrix(i)(j) = False
            Next
        Next

        _h = height
        _w = width
    End Sub
    Sub New(width As Integer, height As Integer, expandMatrix As Boolean)
        Me.New(width, height)
        Me._expMatrix = expandMatrix
    End Sub
    Public Sub SetupMatrix(matrix()() As Boolean)
        If matrix.Length <> _h Then
            Throw InvalidMatrix
        End If
        For i As Integer = 0 To _h - 1
            If matrix(i).Length <> _w Then
                Throw InvalidMatrix
            End If
        Next

        For i As Integer = 0 To _h - 1
            For j As Integer = 0 To _w - 1
                Me.matrix(i)(j) = matrix(i)(j)
            Next
        Next
    End Sub
    Public Sub setValue(b As Boolean, i As Integer, j As Integer)
        matrix(i)(j) = b
    End Sub
#End Region

#Region "Logic"
#Region "Public"
    Public Sub doNextStep()
        Dim needToExpandMatrix As Boolean = False


        'setup new matrix
        Dim newMatrix(_h - 1)() As Boolean
        For i As Integer = 0 To _h - 1
            ReDim newMatrix(i)(_w - 1)
        Next

        'process the new matrix
        For i As Integer = 0 To _h - 1
            For j As Integer = 0 To _w - 1
                Dim n = NumberOfNeighbors(i, j)

                If matrix(i)(j) = False And Between(n, 3, 3) Then
                    newMatrix(i)(j) = 1

                    If i = 0 Or j = 0 Or i = _h - 1 Or j = _w - 1 Then ' if i have new life on the edge of the matrix
                        needToExpandMatrix = True
                    End If

                    Continue For
                End If



                If matrix(i)(j) = True And Not Between(n, 2, 3) Then
                    newMatrix(i)(j) = 0
                    Continue For
                End If

                newMatrix(i)(j) = matrix(i)(j)

                If newMatrix(i)(j) = True And (i = 0 Or j = 0 Or i = _h - 1 Or j = _w - 1) Then ' if i have life on the edge of the matrix
                    needToExpandMatrix = True
                End If
            Next
        Next

        're-write matrix 
        For i As Integer = 0 To _h - 1
            For j As Integer = 0 To _w - 1
                matrix(i)(j) = newMatrix(i)(j)
            Next
        Next

        If needToExpandMatrix And _expMatrix Then
            doExpandMatrix()
        End If

    End Sub
#End Region
#Region "Private"
    Private Sub doExpandMatrix()
        If _h >= _maxMatrixSize Or _w >= _maxMatrixSize Then
            Exit Sub
        End If


        _h += 2
        _w += 2
        ' expand the physical size of the matrix
        ' may have an impact on the performance
        ReDim Preserve matrix(_h - 1)
        For i As Integer = 0 To _h - 1
            ReDim Preserve matrix(i)(_w - 1)
        Next

        ' recenter the content

        'recenter vertical
        For i As Integer = _h - 2 To 1 Step -1
            matrix(i) = matrix(i - 1).Clone
        Next

        'recenter horizontal
        For i As Integer = 1 To _h - 2
            For j As Integer = _w - 2 To 1 Step -1
                matrix(i)(j) = matrix(i)(j - 1)
            Next
        Next

        'clear the border
        For i As Integer = 0 To _h - 1
            For j As Integer = 0 To _w - 1
                matrix(0)(j) = False
                matrix(i)(0) = False
            Next
        Next
    End Sub
    Private Function Between(n As Integer, min As Integer, max As Integer)
        Return (n >= min And n <= max)
    End Function
    Private Function NumberOfNeighbors(i As Integer, j As Integer)
        Dim n As Integer = 0

        '0 0 0
        '0 1 0 
        '0 0 1

        If i > 0 Then 'if it has an upper band
            If j > 0 Then 'upper left 
                If matrix(i - 1)(j - 1) Then
                    n += 1
                End If
            End If
            If j < _w - 1 Then ' upper right
                If matrix(i - 1)(j + 1) Then
                    n += 1
                End If
            End If


            If matrix(i - 1)(j) Then 'upper
                n += 1
            End If

        End If

        If i < _h - 1 Then 'lower band
            If j > 0 Then ' lower left
                If matrix(i + 1)(j - 1) Then
                    n += 1
                End If
            End If
            If j < _w - 1 Then ' lower right
                If matrix(i + 1)(j + 1) Then
                    n += 1
                End If
            End If
            'lower
            If matrix(i + 1)(j) Then
                n += 1
            End If
        End If

        If j > 0 Then ' left
            If matrix(i)(j - 1) Then
                n += 1
            End If
        End If
        If j < _w - 1 Then ' rigth
            If matrix(i)(j + 1) Then
                n += 1
            End If
        End If

        Return n
    End Function
#End Region
#End Region

    Public Sub PrintMatrix()
        Console.Clear()

        For i As Integer = 0 To _h - 1
            For j As Integer = 0 To _w - 1

                If matrix(i)(j) Then
                    Console.BackgroundColor = ConsoleColor.Yellow
                Else
                    Console.BackgroundColor = ConsoleColor.White
                End If
                Console.Write("O ")
            Next
            Console.WriteLine()
        Next
        Console.BackgroundColor = ConsoleColor.Black
        Console.WriteLine()
    End Sub

    Public Function getAtXY(y As Integer, x As Integer)
        Return matrix(y)(x)
    End Function


End Class
