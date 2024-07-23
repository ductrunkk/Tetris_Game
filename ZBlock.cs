namespace Tetris1
{
    public class ZBlock : Block //Lớp con khối Z
    {
        //Lưu trữ các vị trí ô cho bốn trạng thái xoay 
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[] { new (0,0), new (0,1), new (1,1), new (1,2) },
            new Position[] { new (0,2), new (1,1), new (1,2), new (2,1) },
            new Position[] { new (1,0), new (1,1), new (2,1), new (2,2) },
            new Position[] { new (0,1), new (1,0), new (1,1), new (2,0) },
        };
        //Các thuộc tính của block Z:
        //Mã số block Z là 7
        public override int Id => 7;

        //Offset bắt đầu có tọa độ là (0, 3) để khối xuất hiện ở giữa hàng trên cùng
        protected override Position StartOffset => new Position(0, 3);

        //Trả về mảng vị trí ô ở trên
        protected override Position[][] Tiles => tiles;

    }
}
