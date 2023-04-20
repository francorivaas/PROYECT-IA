using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TreeQuestion : ITreeNode
{
    Func<bool> myQuestion;
    ITreeNode myTnode;
    ITreeNode myFnode;

    public TreeQuestion(Func<bool> question/*le paso un func porque necesito un bool*/,ITreeNode Tnode, ITreeNode Fnode) 
    {
        myQuestion = question;  
        myTnode = Tnode;
        myFnode = Fnode;
    }
    public void Execute()
    {
        if (myQuestion())
        {
            //si me da true, ejecuto el nood verdadero
            myTnode.Execute(); 
        }
        else
        {
            //si me da false ejecuto el nodo falso.
            myFnode.Execute();
        }
    }
}
