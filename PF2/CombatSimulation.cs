using System;
using System.Collections.Generic;

namespace PF2
{
    public class TUIBattleFigure : IBattleFigure
    {
        char identifier;
        public TUIBattleFigure(char c)
        {
            identifier = c;
        }
        public void Render(int x, int y)
        {
            Console.CursorLeft = x;
            Console.CursorTop = y;
            Console.Write(identifier);
        }
    }
    public class TUIMapRenderer : BattleMap.IMapRenderer
    {
        public void RenderCell(BattleMap.BattleMapCell cell, int x, int y)
        {
            if ((cell.figures == null)||(cell.figures.Count == 0))
            {
                Console.CursorLeft = x;
                Console.CursorTop = y;
                Console.Write('.');
                return;
            } else {
                cell.figures[0].Render(x,y);
            }
        }
    }
    public interface ICombatSimulationUserInterface
    {
        public IObserver GetObserver();
        public void LogTurn(uint turn);
    }

    public class PF2CombatSimulation
    {
        public PF2CombatSimulation()
        {

        }
        public ICreatureInstance GetValidTarget(InitiativeTracker tracker, int team)
        {
            ICreatureInstance target = null;
            foreach (ICreatureInstance creature in tracker.GetTeam(team))
            {
                if (creature.IsActive())
                {
                    target = creature;
                    break;
                }
            }
            return target;
        }
        public IAction ChooseAction(ICreatureInstance creature)
        {
            IAction action = null;

            if (creature == null) return null;
            foreach (IAction a in creature.GetCreatureInfo().GetActions())
            {
                if (a.GetCost() <= creature.AvailableActions())
                {
                    action = a;
                    break;
                }
            }
            return action;
        }
        public void Run(string monster_name, ICombatSimulationUserInterface ui)
        {
            IObserver observer = ui.GetObserver();
            MonsterInstance enemy1 = MonsterFactory.CreateInstanceByName(monster_name);
            PlayerInstance player = new PlayerInstance();
            player.name = "Player1";
            player.currentHp = player.maxHp = 20;
            player.isAlive = true;
            player.ac = 18;

            IAction strike = new StrikeAction(RangeType.Melee, "Sword", 7, new List<DamageSource>() { new DamageSource(new DieSource(1, DieType.D8, 4),
                                                                                                                       DamageType.Slashing) }, null, null);
            player.actions.Add(strike);
            BasicActions.AddObserver(observer);
            enemy1.AddObserver(observer);
            player.AddObserver(observer);
            uint iteration = 1;
            observer.Message(enemy1.GetName() + "'s current HP: " + enemy1.GetCurrentHP());
            observer.Message(player.GetName() + "'s current HP: " + player.GetCurrentHP());

            InitiativeTracker initiativeTracker = new InitiativeTracker();
            initiativeTracker.AddTurn(enemy1, 1);
            initiativeTracker.AddTurn(player, 0);
            initiativeTracker.AddObserver(observer);
            initiativeTracker.StartInitiativeOrder();
            int[] enemies = new int[2] { 1, 0 };
            TUIBattleFigure playerFigure = new TUIBattleFigure('P');
            TUIBattleFigure monsterFigure = new TUIBattleFigure('M');
            TUIMapRenderer renderer = new TUIMapRenderer();
            BattleMap map = new BattleMap();
            map.Add(playerFigure, 5, 5);
            map.Add(monsterFigure, 9, 8);
            map.SetMapRenderer(renderer);

            while (enemy1.IsActive() && player.IsActive())
            {
                ui.LogTurn(iteration);
                foreach ((ICreatureInstance creature, int team) in initiativeTracker.DoRound())
                {
                    int attackCount = 0;
                    creature.ResetCombatActions(); //Gain Combat Actions

                    while (creature.AvailableActions() > 0)
                    {
                        ICreatureInstance other = GetValidTarget(initiativeTracker, enemies[team]); //Get a target
                        if (other == null) break;
                        IAction action = ChooseAction(creature);
                        if (action == null) break;
                        int modifier = 0;
                        if (action.HasTrait(Traits.Attack))
                        {
                            modifier = attackCount * -5;
                            attackCount += 1;
                        }
                        creature.ConsumeActions(action.GetCost());
                        action.Execute(other, modifier); //Then attack
                    }
                }
                iteration++;
            }
        }
    }
}