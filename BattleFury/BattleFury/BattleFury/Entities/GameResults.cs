using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleFury.Entities.Characters;

namespace BattleFury.Entities
{
    /// <summary>
    /// Store the results of a battle. Characters can tie.
    /// </summary>
    public class GameResults
    {

        public List<Result> results = new List<Result>();

        public GameResults()
        {
        }

        public void SetPlace(Character character, int place)
        {
            results.Add(new Result(character, place));
        }

        #region Result
        public class Result
        {
            public Character character;

            public int place;
            public Result(Character character, int place)
            {
                this.character = character;
                this.place = place;
            }
        }
        #endregion
    }

}
