﻿using HandHistories.Objects.Actions;
using HandHistories.Objects.Cards;
using HandHistories.Objects.GameDescription;
using HandHistories.Parser.UnitTests.Parsers.Base;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HandHistories.Parser.Parsers.Exceptions;
using HandHistories.Parser.Parsers.FastParser.OnGame;

namespace HandHistories.Parser.UnitTests.Parsers.FastParserTests.Pacific
{
    
    using Parser = OnGameFastParserImpl;
    [TestFixture]
    class OnGameFastParserActionTests
    {
        //[Test]
        //public void ParseBlindActionLine_PostingDead_Works()
        //{
        //    HandAction handAction = Parser.ParseBlindAction("BONUS 1OOO posts small blind ($0.50)");

        //    Assert.AreEqual(new HandAction("BONUS 1OOO", HandActionType.POSTS, 0.5m, Street.Preflop), handAction);
        //}

        [Test]
        public void ParseBlindActionLine_PostingSmallBlind_Works()
        {
            HandAction handAction = Parser.ParseBlindAction("BONUS 1OOO posts small blind ($0.50)");

            Assert.AreEqual(new HandAction("BONUS 1OOO", HandActionType.SMALL_BLIND, 0.5m, Street.Preflop), handAction);
        }

        [Test]
        public void ParseBlindActionLine_PostingBigBlind_Works()
        {
            HandAction handAction = Parser.ParseBlindAction("fyabcf posts big blind ($1)");

            Assert.AreEqual(new HandAction("fyabcf", HandActionType.BIG_BLIND, 1m, Street.Preflop), handAction);
        }

        [Test]
        public void ParseRegularActionLine_Bet_Works()
        {
            HandAction handAction = Parser.ParseRegularActionLine("fyabcf bets $18.00", Street.Flop, false);

            Assert.AreEqual(new HandAction("fyabcf", HandActionType.BET, 18m, Street.Flop), handAction);
        }

        [Test]
        public void ParseRegularActionLine_Check_Works()
        {
            HandAction handAction = Parser.ParseRegularActionLine("kliketiklok checks", Street.Flop, false);

            Assert.AreEqual(new HandAction("kliketiklok", HandActionType.CHECK, 0m, Street.Flop), handAction);
        }

        [Test]
        public void ParseRegularActionLine_Call_Works()
        {
            HandAction handAction = Parser.ParseRegularActionLine("kliketiklok calls $15.50", Street.Preflop, false);

            Assert.AreEqual(new HandAction("kliketiklok", HandActionType.CALL, 15.50m, Street.Preflop), handAction);
        }

        [Test]
        public void ParseRegularActionLine_Raise_Works()
        {
            HandAction handAction = Parser.ParseRegularActionLine("BONUS 1OOO raises $1.50 to $2.00", Street.Preflop, false);

            Assert.AreEqual(new HandAction("BONUS 1OOO", HandActionType.RAISE, 1.50m, Street.Preflop), handAction);
        }

        [Test]
        public void ParseRegularActionLine_Fold_Works()
        {
            HandAction handAction = Parser.ParseRegularActionLine("Dbcee89 folds", Street.Preflop, false);

            Assert.AreEqual(new HandAction("Dbcee89", HandActionType.FOLD, 0m, Street.Preflop), handAction);
        }

        [Test]
        public void ParseShowDownActionLine_Wins1kplus_Works()
        {
            List<HandAction> actions = new List<HandAction>();
            Parser.ParseShowdownActions(new string[] { "Main pot: $1,238.40 won by AHULDAHRI ($1,236.40)" }, 0, actions);

            Assert.AreEqual(new WinningsAction("AHULDAHRI", HandActionType.WINS, 1236.40m, 0), actions[0]);
        }

        [Test]
        public void ParseShowDownActionLine_MultipleWinners_Works()
        {
            List<HandAction> actions = new List<HandAction>();
            Parser.ParseShowdownActions(new string[] { "Main pot: $710.00 won by alikator21 ($354.50), McCall901 ($354.50)" }, 0, actions);

            Assert.AreEqual(new WinningsAction("alikator21", HandActionType.WINS, 354.50m, 0), actions[0]);
            Assert.AreEqual(new WinningsAction("McCall901", HandActionType.WINS, 354.50m, 0), actions[1]);
        }

        [Test]
        public void ParseShowDownActionLine_MultipleWinnersSidePot_Works()
        {
            List<HandAction> actions = new List<HandAction>();
            Parser.ParseShowdownActions(new string[] { "Side pot 2: $11.10 won by zatli74 ($5.20), Hurtl ($5.20)" }, 0, actions);

            Assert.AreEqual(new WinningsAction("zatli74", HandActionType.WINS_SIDE_POT, 5.20m, 2), actions[0]);
            Assert.AreEqual(new WinningsAction("Hurtl", HandActionType.WINS_SIDE_POT, 5.20m, 2), actions[1]);
        }
    }
}
