﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FastColoredTextBoxNS
{
    public class SyntaxHighlighter : IDisposable
    {
        //styles
        private static readonly Platform platformType = PlatformType.GetOperationSystemPlatform();
        //


        public static RegexOptions RegexCompiledOption
        {
            get
            {
                if (platformType == Platform.X86)
                    return RegexOptions.Compiled;
                else
                    return RegexOptions.None;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion

        public virtual void AutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            var tb = (sender as FastColoredTextBox);
            Language language = tb.Language;
            switch (language)
            {
                case Language.CSharp:
                    CSharpAutoIndentNeeded(sender, args);
                    break;
                case Language.VB:
                    VBAutoIndentNeeded(sender, args);
                    break;
                case Language.HTML:
                    HTMLAutoIndentNeeded(sender, args);
                    break;
                case Language.Xml:HTMLAutoIndentNeeded(sender, args);
                    break;
                case Language.SQL:
                    SQLAutoIndentNeeded(sender, args);
                    break;
                case Language.PHP:
                    PHPAutoIndentNeeded(sender, args);
                    break;
                case Language.Javascript:
                    CSharpAutoIndentNeeded(sender, args);
                    break; //JS like C#
                case Language.CPP:CSharpAutoIndentNeeded(sender, args);
                    break;
                case Language.Objective_C: CSharpAutoIndentNeeded(sender, args);
                    break;
                case Language.Actionscript: CSharpAutoIndentNeeded(sender, args);
                    break;
                case Language.Scala: CSharpAutoIndentNeeded(sender, args);
                    break;
                case Language.Java: CSharpAutoIndentNeeded(sender, args);
                    break;
            }
        }

        void PHPAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            /*
            FastColoredTextBox tb = sender as FastColoredTextBox;
            tb.CalcAutoIndentShiftByCodeFolding(sender, args);*/
            //block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{.*\}[^""']*$"))
                return;
            //start of block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{"))
            {
                args.ShiftNextLines = args.TabLength;
                return;
            }
            //end of block {}
            if (Regex.IsMatch(args.LineText, @"}[^""']*$"))
            {
                args.Shift = -args.TabLength;
                args.ShiftNextLines = -args.TabLength;
                return;
            }
            //is unclosed operator in previous line ?
            if (Regex.IsMatch(args.PrevLineText, @"^\s*(if|for|foreach|while|[\}\s]*else)\b[^{]*$"))
                if (!Regex.IsMatch(args.PrevLineText, @"(;\s*$)|(;\s*//)")) //operator is unclosed
                {
                    args.Shift = args.TabLength;
                    return;
                }
        }

        protected void SQLAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            var tb = sender as FastColoredTextBox;
            tb.CalcAutoIndentShiftByCodeFolding(sender, args);
        }

        protected void HTMLAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            var tb = sender as FastColoredTextBox;
            tb.CalcAutoIndentShiftByCodeFolding(sender, args);
        }

        protected void VBAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            //end of block
            if (Regex.IsMatch(args.LineText, @"^\s*(End|EndIf|Next|Loop)\b", RegexOptions.IgnoreCase))
            {
                args.Shift = -args.TabLength;
                args.ShiftNextLines = -args.TabLength;
                return;
            }
            //start of declaration
            if (Regex.IsMatch(args.LineText,
                              @"\b(Class|Property|Enum|Structure|Sub|Function|Namespace|Interface|Get)\b|(Set\s*\()",
                              RegexOptions.IgnoreCase))
            {
                args.ShiftNextLines = args.TabLength;
                return;
            }
            // then ...
            if (Regex.IsMatch(args.LineText, @"\b(Then)\s*\S+", RegexOptions.IgnoreCase))
                return;
            //start of operator block
            if (Regex.IsMatch(args.LineText, @"^\s*(If|While|For|Do|Try|With|Using|Select)\b", RegexOptions.IgnoreCase))
            {
                args.ShiftNextLines = args.TabLength;
                return;
            }

            //Statements else, elseif, case etc
            if (Regex.IsMatch(args.LineText, @"^\s*(Else|ElseIf|Case|Catch|Finally)\b", RegexOptions.IgnoreCase))
            {
                args.Shift = -args.TabLength;
                return;
            }

            //Char _
            if (args.PrevLineText.TrimEnd().EndsWith("_"))
            {
                args.Shift = args.TabLength;
                return;
            }
        }

        protected void CSharpAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            //block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{.*\}[^""']*$"))
                return;
            //start of block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{"))
            {
                args.ShiftNextLines = args.TabLength;
                return;
            }
            //end of block {}
            if (Regex.IsMatch(args.LineText, @"}[^""']*$"))
            {
                args.Shift = -args.TabLength;
                args.ShiftNextLines = -args.TabLength;
                return;
            }
            //label
            if (Regex.IsMatch(args.LineText, @"^\s*\w+\s*:\s*($|//)") &&
                !Regex.IsMatch(args.LineText, @"^\s*default\s*:"))
            {
                args.Shift = -args.TabLength;
                return;
            }
            //some statements: case, default
            if (Regex.IsMatch(args.LineText, @"^\s*(case|default)\b.*:\s*($|//)"))
            {
                args.Shift = -args.TabLength/2;
                return;
            }
            //is unclosed operator in previous line ?
            if (Regex.IsMatch(args.PrevLineText, @"^\s*(if|for|foreach|while|[\}\s]*else)\b[^{]*$"))
                if (!Regex.IsMatch(args.PrevLineText, @"(;\s*$)|(;\s*//)")) //operator is unclosed
                {
                    args.Shift = args.TabLength;
                    return;
                }
        }
        private void RestoreBrackets(FastColoredTextBox tb, IList<char> oldBrackets)
        {
            tb.LeftBracket = oldBrackets[0];
            tb.RightBracket = oldBrackets[1];
            tb.LeftBracket2 = oldBrackets[2];
            tb.RightBracket2 = oldBrackets[3];
        }

        private char[] RememberBrackets(FastColoredTextBox tb)
        {
            return new[] {tb.LeftBracket, tb.RightBracket, tb.LeftBracket2, tb.RightBracket2};
        }
    }

    /// <summary>
    /// Language
    /// </summary>
    public enum Language
    {
        Text,
        HTML,
        ASP,
        Javascript,
        CSS,
        PHP,
        SQL,
        Batch,
        INI,
        JSON,
        Actionscript,
        Antlr,
        Assembly,
        CSharp,
        C,
        CPP,
        D,
        Diff,
        FSharp,
        Java,
        Lua,
        Lisp,
        Makefile,
        Objective_C,
        Pascal,
        Perl,
        Python,
        Ruby,
        Scala,
        Scheme,
        Shell,
        QBasic,
        VB,
        Xml,
        Yaml,
    }
}