using System.Collections.Generic;
using UnityEngine;

public interface ICombinationChecker
{
    CombinationResult Check(IReadOnlyList<Card> cards);
    string CombinationName { get; } 
}
                                                                    