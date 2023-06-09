using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Elementalium.Domain.Model
{
    public class Enums
    {
        public enum Grabs
        {
            ReverseGrip,
            UpperGrip,
            LowerGrip,
            Grip,
        }

        public enum Families
        {
            Intercept,
            ClimbHorizontalBar,
            Binder,
            Degrees,
            Span,
            Turnover,
            Dismount,
            Static,
            None
        }

        public enum Positions
        {
            Vis,
            BehindVis,
            Sed,
            Emphasis,
            EmphasisBehind,
            Earth
        }

        public enum Features
        {
            LegSweep,
            Inertia,
            Knees,
            CrankingShoulders,
            Somersault,
            Fear,
            Power
        }

        public static string GetFamiliesRegexFormat()
        {
            return @"^[0-7]{1}$";
        }
    }
}
