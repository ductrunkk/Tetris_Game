namespace Tetris1
{
    public class IBlock : Block //Lớp con khối I
    {
        //Lưu trữ các vị trí ô cho bốn trạng thái xoay 
        private readonly Position[][] tiles = new Position[][] 
        {
            new Position[] { new (1,0), new (1,1), new (1,2), new (1,3) },
            new Position[] { new (0,2), new (1,2), new (2,2), new (3,2) },
            new Position[] { new (2,0), new (2,1), new (2,2), new (2,3) },
            new Position[] { new (0,1), new (1,1), new (2,1), new (3,1) },
        };
        
        //Các thuộc tính của block I:
        // mã số block I là 1
        public override int Id => 1; 
        
        //Offset bắt đầu có tọa độ là (-1,3) để khối xuất hiện ở giữa hàng trên cùng
        protected override Position StartOffset => new Position(-1, 3);
        
        //Trả về mảng vị trí ô ở trên
        protected override Position[][] Tiles => tiles;
    }
}
