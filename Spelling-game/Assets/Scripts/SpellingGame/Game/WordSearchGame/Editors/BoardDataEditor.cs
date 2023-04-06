using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Random = System.Random;
using System;


[CustomEditor(typeof(BoardData), false), CanEditMultipleObjects]
public class BoardDataEditor : Editor
{
    private BoardData boardData => target as BoardData;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        BoardInitialisation();
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space();    

        EditorGUILayout.LabelField("Board");

        EditorGUILayout.Space();

        LoadSavedBoardButton();
        DeleteSavedBoardButton();

        if (boardData.GetBoardData() != null && boardData.Rows > 0 && boardData.Columns > 0)
        {
            DrawBoard();
        }

        if (boardData.GetBoardData() != null)
        {
            ResetBoardButton();
            SaveBoardButton();
        }

        AutoFillButton();

        EditorGUILayout.LabelField("Word to be searched");

        DrawDefaultInspector();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(boardData);

        }
    }

    //The function of the code is taken from
    //CodePlanStudio, C. (Director). (2020, July 27). Words Spy Game Episode 4 | Unity Word Searching Game [Video file]. Retrieved February 10, 2023, from https://www.youtube.com/watch?v=5XCDkd61-i8&amp;list=PLJLLSehgFnspMBk7VaLI18Digsj2xuMhT&amp;index=4
    //Get all the value and information needed to build the board
    private void BoardInitialisation()
    {
        var previousRows = boardData.Rows;
        var previousColumns = boardData.Columns;

        boardData.Rows = EditorGUILayout.IntField("Rows", boardData.Rows);
        boardData.Columns = EditorGUILayout.IntField("Columns", boardData.Columns);

        if ((boardData.Columns != previousColumns || boardData.Rows != previousRows) && boardData.Columns > 0 && boardData.Rows > 0)
        {
            boardData.CreateNewBoard();
        }
    }

    //The function of the code is taken from
    //CodePlanStudio, C. (Director). (2020, July 27). Words Spy Game Episode 4 | Unity Word Searching Game [Video file]. Retrieved February 10, 2023, from https://www.youtube.com/watch?v=5XCDkd61-i8&amp;list=PLJLLSehgFnspMBk7VaLI18Digsj2xuMhT&amp;index=4
    //Draw a board
    private void DrawBoard()
    {
        var tableStyle = new GUIStyle("box")
        {
            padding = new RectOffset(10,10,10,10)
        };

        var rowStyle = new GUIStyle
        {
            fixedHeight = 30,
            fixedWidth = 30,
            alignment = TextAnchor.MiddleCenter
        };

        var columnStyle = new GUIStyle
        {
            fixedWidth = 30
        };

        EditorGUILayout.BeginHorizontal(tableStyle);

        for (int col = 0; col < boardData.Columns; col++)
        {
            EditorGUILayout.BeginVertical(columnStyle);
            for (int row = 0; row < boardData.Rows; row++)
            {
                EditorGUILayout.BeginHorizontal(rowStyle);
                var character = (string)EditorGUILayout.TextArea(boardData.GetCell(col,row).Val);
                boardData.UpdateBoardData(col, row, character);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndHorizontal();

    }

    private void LoadSavedBoardButton()
    {
        if(GUILayout.Button("Load Saved Board"))
        {
            boardData.LoadSavedBoard();
            DrawBoard();
        }
    }

    private void ResetBoardButton()
    {
        if(GUILayout.Button("Reset Board"))
        {
            boardData.ClearBoard();
        }
    }

    private void SaveBoardButton()
    {
        if (GUILayout.Button("Save Board"))
        {
            boardData.DeleteSavedBoard();
            string ID = GenerateID();
            StandardiseBoard();
            boardData.SaveBoard(ID);
        }
    }

    private void DeleteSavedBoardButton()
    {
        if (GUILayout.Button("Delete Saved Board"))
        {
            boardData.ClearBoard();
            boardData.DeleteSavedBoard();   
        }
    }

    //After having filled all the words wanted on the board, auto-fill random alphabet to the empty cell
    private void AutoFillButton()
    {
        string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        if (GUILayout.Button("Auto-fill board"))
        {
            for (int i = 0; i < boardData.Columns; i++)
            {
                for (int j = 0; j < boardData.Rows; j++)
                {
                    if (boardData.GetCell(i, j).Val == "")
                    {
                        Random rnd = new();
                        int index = rnd.Next(0, alphabets.Length);
                        boardData.UpdateBoardData(i, j, alphabets[index].ToString());
                        //boardData.GetCell(i, j).Val = alphabets[index].ToString();
                        //EditorUtility.SetDirty(boardData);
                    }
                }
            }
        }
    }

    //Ensuring all the letters is uppercase and have only one letter
    private void StandardiseBoard()
    {
        for (int i = 0; i < boardData.Columns; i++)
        {
            for (int j = 0; j < boardData.Rows; j++)
            {
                if (boardData.GetCell(i, j).Val != "")
                {
                    boardData.UpdateBoardData(i, j, boardData.GetCell(i, j).Val[0].ToString().ToUpper());
                }
            }
        }
    }

    //Generate random unique ID for the board
    private string GenerateID()
    {
        Guid guid = Guid.NewGuid();
        return guid.ToString();
    }
}
