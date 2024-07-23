namespace Tetris1
{
    public class OBlock : Block //Lớp con khối O
    {
        //Khối O chiếm cùng một vị trí trong mọi trạng thái xoay
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[] { new(0,0), new(0,1), new(1,0), new(1,1) }
        };
        //Các thuộc tính của block O:
        //Mã số block O là 4
        public override int Id => 4;
        
         //Offset bắt đầu có tọa độ là (0,4) để khối xuất hiện ở giữa hàng trên cùng
        protected override Position StartOffset => new Position(0,4);
        
        //Trả về mảng vị trí ô ở trên
        protected override Position[][] Tiles => tiles;
    }
}
