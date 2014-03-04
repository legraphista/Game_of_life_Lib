Module Main

    Sub Main()

        Dim gol As New GameOfLife.GameOfLife(20, 20, True)
        '        	0	1	2	3	4	5	6	7	8	9	10	11	12	13	14	15	16	17	18	19
        '0:
        '1:
        '2																0				
        '3														0		0	0			
        '4														0		0				
        '5														0						
        '6												0								
        '7										0		0								
        '8:
        '9:
        '10:
        '11:
        '12:
        '13:
        '14:
        '15:
        '16:
        '17:
        '18:
        '19:

        gol.setValue(True, 2 + 10, 15 - 9)
        gol.setValue(True, 3 + 10, 15 - 9)
        gol.setValue(True, 4 + 10, 15 - 9)
        gol.setValue(True, 3 + 10, 16 - 9)

        gol.setValue(True, 3 + 10, 13 - 9)
        gol.setValue(True, 4 + 10, 13 - 9)
        gol.setValue(True, 5 + 10, 13 - 9)

        gol.setValue(True, 6 + 10, 11 - 9)
        gol.setValue(True, 7 + 10, 11 - 9)

        gol.setValue(True, 7 + 10, 9 - 9)
        'gol.setValue(1, 0, 7)
        'gol.setValue(1, 1, 7)
        'gol.setValue(1, 2, 7)
        'gol.setValue(1, 2, 8)
        'gol.setValue(1, 1, 9)

        Do
            gol.PrintMatrix()
            gol.doNextStep()
            Threading.Thread.Sleep(100)
        Loop



    End Sub




End Module
