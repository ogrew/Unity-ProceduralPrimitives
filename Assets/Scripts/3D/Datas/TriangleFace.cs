public class TriangleFace
{
		public int v1 {set; get;}
		public int v2 {set; get;}
		public int v3 {set; get;}

		public TriangleFace(int v1, int v2, int v3) {
			this.v1 = v1;
			this.v2 = v2;
			this.v3 = v3;
		}

		public void SetV1(int newV1) { v1 = newV1;}
		public void SetV2(int newV2) { v2 = newV2;}
		public void SetV3(int newV3) { v3 = newV3;}
}
