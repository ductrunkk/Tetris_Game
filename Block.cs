using System.Collections.Generic;

namespace Tetris1
{
    public abstract class Block //khởi tạo 
    {
        //Tạo vị trí cho từng tile(ô)
        protected abstract Position[][] Tiles { get; } 
        
        //Vị trí cho khối đầu tiên bắt đầu xuất hiện trong grid(lưới)
        protected abstract Position StartOffset { get; } 

        //ID để xác định từng loại block
        public abstract int Id { get; } 

        //Lưu trữ trạng thái xoay hiện tại 
        private int rotationState;

        //Lưu trữ offset(khoảng cách so với rìa) hiện tại
        private Position offset; 
    
        public Block()
        {
            // khởi tạo cho offset bằng với offset ban đầu
            offset = new Position(StartOffset.Row, StartOffset.Column); 
        }

        //phương thức trả về các vị trí ô trong lưới mà khối chiếm trong phép xoay và offset hiện tại
        public IEnumerable<Position> TilePositions()  
        {
            foreach (Position p in Tiles[rotationState])
            {
                yield return new Position(p.Row + offset.Row, p.Column + offset.Column);
            }
        }

        //Phương thức xoay khối 90 độ theo chiều kim đồng hồ 
        public void RotateCW()
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }

        //Phương thức xoay khối 90 độ ngược chiều kim đồng hồ 
        public void RotateCCW()
        {
            if(rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState--;
            }
        }

        //Phương thức để di chuyển khối theo 1 hàng và 1 cột cố định
        public void Move(int rows, int columns)
        {
            offset.Row += rows;
            offset.Column += columns;
        }

        //Phương Thức để đặt lại độ xoay và vị trí ban đầu
        public void Reset()
        {
            rotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column;
        }
    }
}
