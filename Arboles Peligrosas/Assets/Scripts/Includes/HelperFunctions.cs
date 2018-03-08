using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFunctions {
    public static bool containsTag(string tagToFind, string tagsToSearchStr) {
        string[] tagsToSearch = tagsToSearchStr.Split(',');

        foreach (string tagtoSearch in tagsToSearch) {
            if (tagToFind == tagtoSearch) return true;
        }

        return false;
    }

    public static bool containsTag(List<string> tagsToFind, string tagsToSearchStr) {
        string[] tagsToSearch = tagsToSearchStr.Split(',');

        foreach (string tagToFind in tagsToFind) {
            foreach (string tagtoSearch in tagsToSearch) {
                if (tagToFind == tagtoSearch) return true;
            }
        }

        return false;
    }

    public static void addToDict(Dictionary<string, string> dict, string key, string val)
    {
        string tmp;
        if (dict.TryGetValue(key, out tmp))
            dict[key] = val;
        else
            dict.Add(key, val);
    }
}
