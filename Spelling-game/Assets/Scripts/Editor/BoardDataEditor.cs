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

    private void OnEnable()
    {
    }
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
                        boardData.GetCell(i, j).Val = alphabets[index].ToString();
                    }
                }
            }
        }
    }

    private void StandardiseBoard()
    {
        for (int i = 0; i < boardData.Columns; i++)
        {
            for (int j = 0; j < boardData.Rows; j++)
            {
                if (boardData.GetCell(i, j).Val != "")
                {
                    boardData.GetCell(i, j).Val = boardData.GetCell(i, j).Val[0].ToString().ToUpper();
                }
            }
        }
    }

    private string GenerateID()
    {
        Guid guid = Guid.NewGuid();
        return guid.ToString();
    }
}
