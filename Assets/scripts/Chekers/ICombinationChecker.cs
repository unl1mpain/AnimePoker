using System.Collections.Generic;
using UnityEngine;

public interface ICombinationChecker
{
    CombinationResult Check(List<Card> hand, List<Card> table);
    string CombinationName { get; } 
}
