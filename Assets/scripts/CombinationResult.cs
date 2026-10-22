using System;
using System.Collections.Generic;

public class CombinationResult : IComparable<CombinationResult>
{
    public CombinationCategory Category {  get; }
    public List<Rank> Tiebreakers { get; }  

    public CombinationResult(CombinationCategory category, List<Rank> tiebreakers)
    {
        Category = category;
        Tiebreakers = new List<Rank>(tiebreakers);
    }

    public int CompareTo(CombinationResult result) 
    {
        if (result == null) return 1;

        int categoryComparison = Category.CompareTo(result.Category);
        if (categoryComparison != 0)
        {
            return categoryComparison;
        }
        
        for (int i = 0; i < Tiebreakers.Count; i++) 
        {
            if (Tiebreakers[i] != result.Tiebreakers[i]) 
            { 
                return Tiebreakers[i].CompareTo(result.Tiebreakers[i]);
            }    
        }

        return 0;
    }
}
