using System.Collections.Generic;

namespace PF2
{
    public interface IBattleFigure
    {
        void Render(int x, int y);
    }
    public class BattleMap
    {
        public interface IBattleMapProvider {
            public BattleMap GetCurrentBattleMap();
        }
        public interface IMapRenderer
        {
            void RenderCell(BattleMapCell cell, int x, int y);
        }
        public enum TerrainType
        {
            Normal=0,
            Difficult
        }
        public enum BlockingStatus
        {
            NoCover=0,
            LesserCover,
            NormalCover,
            GreaterCover,
        }
        public struct BattleMapCell
        {
            public List<IBattleFigure> figures;
            public TerrainType terrain;
            public BlockingStatus blocking;
        }
        struct BattleMapCellPosition
        {
            public int x; public int y; public int z;
            public BattleMapCellPosition(int nx, int ny, int nz)
            {
                x = nx; y = ny; z = nz;
            }
        }
        protected IMapRenderer mapRenderer;
        Dictionary<IBattleFigure, BattleMapCellPosition> figurePosition;
        
        BattleMapCell[,] battleMap;

        public BattleMap(int width=10, int height=10)
        {
            battleMap = new BattleMapCell[width,height];
            figurePosition = new Dictionary<IBattleFigure, BattleMapCellPosition>();
        }
        public void SetMapRenderer(IMapRenderer mr)
        {
            mapRenderer = mr;
        }
        public void Add(IBattleFigure figure, int x, int y)
        {
            if (figurePosition.ContainsKey(figure)) return;
            if ((x < 0) || (y < 0)) return;
            if ((x > battleMap.GetLength(0)) || (y > battleMap.GetLength(1))) return;
            BattleMapCellPosition newPos = new BattleMapCellPosition(x, y, 0);
            if (battleMap[x, y].figures == null)
            {
                battleMap[x, y].figures = new List<IBattleFigure>();
            }
            newPos.z = battleMap[x, y].figures.Count;
            battleMap[x, y].figures.Add(figure);
            figurePosition[figure] = newPos;
        }
        public void Remove(IBattleFigure figure)
        {
            if (figurePosition.ContainsKey(figure))
            {
                BattleMapCellPosition pos = figurePosition[figure];
                battleMap[pos.x, pos.y].figures.Remove(figure);
                figurePosition.Remove(figure);
            }
        }
        public void Move(IBattleFigure figure, int deltax, int deltay)
        {
            if (figurePosition.ContainsKey(figure))
            {
                BattleMapCellPosition pos = figurePosition[figure];
                Remove(figure);
                //Add it to the new position
                Add(figure, pos.x + deltax, pos.y + deltay);
            }
        }
        public void Draw()
        {
            if (mapRenderer == null) return;
            for (int i=0; i<battleMap.GetLength(0); i++)
            {
                for(int j=0; j<battleMap.GetLength(1); j++)
                {
                    mapRenderer.RenderCell(battleMap[i,j], i,j);
                }
            }
        }
    }
}