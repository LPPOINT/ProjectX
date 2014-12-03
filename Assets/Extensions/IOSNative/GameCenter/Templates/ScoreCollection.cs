////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////


using System.Collections.Generic;

namespace Assets.Extensions.IOSNative.GameCenter.Templates
{
    public class ScoreCollection {

	
        public Dictionary<int, GCScore> AllTimeScores =  new Dictionary<int, GCScore>();
        public Dictionary<int, GCScore> WeekScores =  new Dictionary<int, GCScore>();
        public Dictionary<int, GCScore> TodayScores =  new Dictionary<int, GCScore>();

    }
}

