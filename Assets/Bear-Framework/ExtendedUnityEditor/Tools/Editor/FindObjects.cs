﻿using UnityEngine;
using UnityEditor;
using System.Collections;

namespace BearFramework.Tools
{
    public class FindObjects : ScriptableWizard
    {
        public enum SearchType
        {
            Name,
            Component,
            Tag
        }

        public string searchFor = "";
        public SearchType searchBy = SearchType.Name;
        public bool caseSensitive = false;
        public bool wholeWord = false;
        public bool inSelectionOnly = false;
        static string lastSearch = "";
        static SearchType lastSearchType = SearchType.Name;
        static bool lastSearchWhole = false;
        static ArrayList foundItems;
        static int foundIndex = -1;

        [MenuItem("Bear Framework/> Find... %&f")]

        static void FindMenuItem()
        {
            ScriptableWizard.DisplayWizard("Find", typeof(FindObjects), "Find", "");
        }
        
        void OnWizardCreate()
        {            
            lastSearch = searchFor;
            lastSearchType = searchBy;
            lastSearchWhole = wholeWord;
            Object[] allObjects;
            foundItems = new ArrayList();

            if (inSelectionOnly)
                allObjects = Selection.objects;
            else
                allObjects = FindObjectsOfTypeAll(typeof(GameObject));

            if (searchBy == SearchType.Name)
            {
                if (wholeWord)
                {
                    if (caseSensitive)
                    {
                        foreach (GameObject anObject in allObjects)
                            if (anObject.name.ToLower().Equals(lastSearch.ToLower()))
                                foundItems.Add(anObject);
                    }
                    else
                    {
                        foreach (GameObject anObject in allObjects)
                            if (anObject.name.Equals(lastSearch))
                                foundItems.Add(anObject);
                    }
                    if (foundItems.Count == 0)
                    {
                        Debug.Log("No active objects were found with the name \"" + lastSearch + "\"");
                        foundIndex = -1;
                    }
                    else
                    {
                        foundIndex = 0;
                        SelectObject(0);
                        AnnounceResult();
                    }
                }
                else
                {
                    if (caseSensitive)
                    {
                        foreach (GameObject anObject in allObjects)
                            if (anObject.name.IndexOf(lastSearch) > -1)
                                foundItems.Add(anObject);
                    }
                    else
                    {
                        foreach (GameObject anObject in allObjects)
                            if (anObject.name.ToLower().IndexOf(lastSearch.ToLower()) > -1)
                                foundItems.Add(anObject);
                    }
                    if (foundItems.Count == 0)
                    {
                        Debug.Log("No active objects were found with names containing \"" + lastSearch + "\"");
                        foundIndex = -1;
                    }
                    else
                    {
                        foundIndex = 0;
                        SelectObject(0);
                        AnnounceResult();
                    }
                }
            }
            else if (searchBy == SearchType.Component)
            { 
                foreach (GameObject objectByType in allObjects)
                    if (objectByType.GetComponent(lastSearch))
                        foundItems.Add(objectByType);
                if (foundItems.Count == 0)
                {
                    Debug.Log("No active objects were found with attached " +
                                "component \"" + lastSearch + "\"");
                    foundIndex = -1;
                }
                else
                {
                    foundIndex = 0;
                    SelectObject(0);
                    AnnounceResult();
                }

            }
            else if (searchBy == SearchType.Tag)
            {
                foreach (GameObject objectByType in allObjects)
                {
                    if (objectByType.tag == lastSearch)
                        foundItems.Add(objectByType);
                    if (foundItems.Count == 0)
                    {
                        Debug.Log("No active objects were found with attached " +
                                    "tag \"" + lastSearch + "\"");
                        foundIndex = -1;
                    }
                    else
                    {
                        foundIndex = 0;
                        SelectObject(0);
                        AnnounceResult();
                    }
                }
            }
        }
        void OnWizardUpdate()
        {
            if (searchFor.Equals(""))
            {
                errorString = "Enter a search and push enter";
                isValid = false;
            }
            else
            {
                errorString = "";
                isValid = true;
            }
            if (searchBy == SearchType.Name)
            {
                helpString = "";
            }
            else
            {
                if (!caseSensitive || !wholeWord)
                {
                    caseSensitive = wholeWord = true;
                }
                helpString = "Component searches always require an exact match";
            }
        }
        [MenuItem("Bear Framework/> Next Result %g")]

        static void NextResultMenuItem()
        {
            if (++foundIndex >= foundItems.Count)
                foundIndex = 0;
            SelectObject(foundIndex);
            AnnounceResult();
        }
        [MenuItem("Bear Framework/> Next Result %g", true)]

        static bool ValidateNextResult()
        {
            return foundIndex > -1;
        }
        [MenuItem("Bear Framework/> Previous Result #%g")]

        static void PreviousResultMenuItem()
        {
            if (--foundIndex < 0)
                foundIndex = foundItems.Count - 1;
            SelectObject(foundIndex);
            AnnounceResult();
        }
        [MenuItem("Bear Framework/> Previous Result #%g", true)]

        static bool ValidatePreviousResult()
        {
            return foundIndex > -1;
        }
        static void SelectObject(int newSelection)
        {
            Object[] newSelectionArray = { foundItems[newSelection] as Object };
            Selection.objects = newSelectionArray;
        }
        static void AnnounceResult()
        {
            if (lastSearchType == SearchType.Component)
                Debug.Log("Object " + (foundIndex + 1) + " of " + foundItems.Count +
                            " with attached component \"" + lastSearch + "\"");
            else if (lastSearchWhole)
                Debug.Log("Object " + (foundIndex + 1) + " of " + foundItems.Count +
                            " with the name \"" + lastSearch + "\"");
            else
                Debug.Log("Object " + (foundIndex + 1) + " of " + foundItems.Count +
                            " with name containing \"" + lastSearch + "\"");
        }
    }
}