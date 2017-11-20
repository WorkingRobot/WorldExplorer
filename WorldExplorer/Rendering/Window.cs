using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spark.GL;
using SparseCollections;

namespace WorldExplorer
{
    sealed class Wind
    {
        Window window;
        List<GameObject> regions = new List<GameObject>();
        public Wind(World world) {
            window = new Window(500,500,"WorldExplorer");
            window.Wake();
            int texId = window.loadImage("C:/Users/Aleks.Aleks-PC/Documents/Visual Studio 2017/Projects/Spark.GL/TestProject/opentksquare.png");
            foreach(KeyValuePair<ComparableTuple2<int,int>,RegionFile> region in world.regions)
            {
                GameObject reg = new GameObject("r" + region.Key.Item0 + "." + region.Key.Item1);
                foreach(KeyValuePair<ComparableTuple2<int, int>, Column> column in region.Value.columns)
                {
                    GameObject col = new GameObject("c" + column.Key.Item0 + "." + column.Key.Item1);
                    col.SetParent(reg);
                    foreach (KeyValuePair<ComparableTuple3<byte, byte, byte>, Block> block in column.Value.blocks)
                    {
                        Block b = block.Value;
                        int x, y, z;
                        x = b.x + column.Value.xPos * 16;
                        z = b.z + column.Value.zPos * 16;
                        y = b.y;
                        Block _t;
                        Mesh m = CreateCustomMesh(
                            !column.Value.blocks.TryGetValue(b.x, (byte)(b.y + 1), b.z, out _t),
                            !column.Value.blocks.TryGetValue(b.x, (byte)(b.y - 1), b.z, out _t),
                            !column.Value.blocks.TryGetValue(b.x, b.y, (byte)(b.z-1), out _t),
                            !column.Value.blocks.TryGetValue(b.x, b.y, (byte)(b.z + 1), out _t),
                            !column.Value.blocks.TryGetValue((byte)(b.x-1), b.y, b.z, out _t),
                            !column.Value.blocks.TryGetValue((byte)(b.x+1), b.y, b.z, out _t));
                        if (m.Triangles.Length == 0) continue;
                        GameObject bGO = new GameObject("b" + x + "," + y + "," + z);
                        MeshFilter mf = bGO.AddComponent<MeshFilter>();
                        mf.mesh = m;
                        bGO.transform.Position = new Vec3(x, y, z);
                        bGO.SetParent(col);
                    }
                    MeshFilter mF = col.AddComponent<MeshFilter>();
                    mF.mesh = combineMeshes(col.GetChildren());
                    Material mat = new Material(new Vec3(0.7, 0.7, 0.7), new Vec3(0, 0, 0), new Vec3(0.9, 0.9, 0.9));
                    mF.material = mat;
                    mF.material.TextureID = texId;
                    col.AddComponent<MeshRenderer>();
                    col.transform.Position = new Vec3();
                    foreach(GameObject go in col.GetChildren())
                    {
                        go.Delete();
                    }
                }
            }
            Camera camera = new PerspectiveCamera(0.1f, 1000, 1.3f);
            camera.Position = new Vec3(0, 0, 0);
            window.SetCamera(camera);
            GameObject cam = new GameObject("cam");
            var t = cam.AddComponent<CameraRotationThing>();
            t.w = window;
            t.cam = camera;
            window.Run();
            
        }
        private Mesh CreateCustomMesh(bool up, bool down, bool left, bool right, bool front, bool back)
        {
            
            Vec3[] verts = new Vec3[]
            {
                //left
                new Vec3(-0.5f, -0.5f,  -0.5f),
                new Vec3(0.5f, 0.5f,  -0.5f),
                new Vec3(0.5f, -0.5f,  -0.5f),
                new Vec3(-0.5f, 0.5f,  -0.5f),

                //back
                new Vec3(0.5f, -0.5f,  -0.5f),
                new Vec3(0.5f, 0.5f,  -0.5f),
                new Vec3(0.5f, 0.5f,  0.5f),
                new Vec3(0.5f, -0.5f,  0.5f),

                //right
                new Vec3(-0.5f, -0.5f,  0.5f),
                new Vec3(0.5f, -0.5f,  0.5f),
                new Vec3(0.5f, 0.5f,  0.5f),
                new Vec3(-0.5f, 0.5f,  0.5f),

                //top
                new Vec3(0.5f, 0.5f,  -0.5f),
                new Vec3(-0.5f, 0.5f,  -0.5f),
                new Vec3(0.5f, 0.5f,  0.5f),
                new Vec3(-0.5f, 0.5f,  0.5f),

                //front
                new Vec3(-0.5f, -0.5f,  -0.5f),
                new Vec3(-0.5f, 0.5f,  0.5f),
                new Vec3(-0.5f, 0.5f,  -0.5f),
                new Vec3(-0.5f, -0.5f,  0.5f),

                //bottom
                new Vec3(-0.5f, -0.5f,  -0.5f),
                new Vec3(0.5f, -0.5f,  -0.5f),
                new Vec3(0.5f, -0.5f,  0.5f),
                new Vec3(-0.5f, -0.5f,  0.5f)
            };
            List<Vec3> tris = new List<Vec3>();
            if (left)
            {
                tris.AddRange(new Vec3[] { new Vec3(0, 1, 2), new Vec3(0, 3, 1) });
            }
            if (back)
            {
                tris.AddRange(new Vec3[] { new Vec3(4, 5, 6), new Vec3(4, 6, 7) });
            }
            if (right)
            {
                tris.AddRange(new Vec3[] { new Vec3(8,9,10), new Vec3(8,10,11) });
            }
            if (up)
            {
                tris.AddRange(new Vec3[] { new Vec3(13,14,12), new Vec3(13,15,14) });
            }
            if (front)
            {
                tris.AddRange(new Vec3[] { new Vec3(16,17,18), new Vec3(16,19,17) });
            }
            if (down)
            {
                tris.AddRange(new Vec3[] { new Vec3(20,21,22), new Vec3(20,22,23) });
            }
            Vec3[] triangles = tris.ToArray();
            Vec3[] colors = new Vec3[]
            {
                //left
                new Vec3(0, 0,  0),
                new Vec3(0, 0,  0),
                new Vec3(0, 0,  0),
                new Vec3(0, 0,  0),

                //back
                new Vec3(0, 0,  0),
                new Vec3(0, 0,  0),
                new Vec3(0, 0,  0),
                new Vec3(0, 0,  0),

                //right
                new Vec3(0, 0,  0),
                new Vec3(0, 0,  0),
                new Vec3(0, 0,  0),
                new Vec3(0, 0,  0),

                //top
                new Vec3(0, 0,  0),
                new Vec3(0, 0,  0),
                new Vec3(0, 0,  0),
                new Vec3(0, 0,  0),

                //front
                new Vec3(0, 0,  0),
                new Vec3(0, 0,  0),
                new Vec3(0, 0,  0),
                new Vec3(0, 0,  0),

                //bottom
                new Vec3(0, 0,  0),
                new Vec3(0, 0,  0),
                new Vec3(0, 0,  0),
                new Vec3(0, 0,  0)
            };
            Vec2[] textures = new Vec2[]
            {
                // left
                new Vec2(0.0f, 0.0f),
                new Vec2(-1.0f, 1.0f),
                new Vec2(-1.0f, 0.0f),
                new Vec2(0.0f, 1.0f),
 
                // back
                new Vec2(0.0f, 0.0f),
                new Vec2(0.0f, 1.0f),
                new Vec2(-1.0f, 1.0f),
                new Vec2(-1.0f, 0.0f),
 
                // right
                new Vec2(-1.0f, 0.0f),
                new Vec2(0.0f, 0.0f),
                new Vec2(0.0f, 1.0f),
                new Vec2(-1.0f, 1.0f),
 
                // top
                new Vec2(0.0f, 0.0f),
                new Vec2(0.0f, 1.0f),
                new Vec2(-1.0f, 0.0f),
                new Vec2(-1.0f, 1.0f),
 
                // front
                new Vec2(0.0f, 0.0f),
                new Vec2(1.0f, 1.0f),
                new Vec2(0.0f, 1.0f),
                new Vec2(1.0f, 0.0f),
 
                // bottom
                new Vec2(0.0f, 0.0f),
                new Vec2(0.0f, 1.0f),
                new Vec2(-1.0f, 1.0f),
                new Vec2(-1.0f, 0.0f)
            };
            return new Mesh(verts, triangles, colors, textures);
        }

        private Mesh combineMeshes(params GameObject[] meshes)
        {
            List<Vec3> verts = new List<Vec3>();
            List<Vec3> colors = new List<Vec3>();
            List<Vec2> texs = new List<Vec2>();
            List<Vec3> tris = new List<Vec3>();
            int offset = 0;
            foreach (GameObject go in meshes)
            {
                Vec3 pos = go.transform.Position;
                Mesh mesh = go.GetComponent<MeshFilter>().mesh;
                verts.AddRange(mesh.Verticies.Select(r => r + pos));
                colors.AddRange(Enumerable.Repeat(new Vec3(), mesh.faces.Count));
                texs.AddRange(mesh.Textures);
                tris.AddRange(mesh.Triangles.Select(r => r += new Vec3(offset,offset,offset)));
                offset += mesh.Verticies.Length;
            }
            return new Mesh(verts.ToArray(), tris.ToArray(), colors.ToArray(), texs.ToArray());
        }
    }
    public class CameraRotationThing : Component
    {
        public Window w;
        public Camera cam;
        public Vec2 lastmousepos;
        int t;
        const float speed = 0.01f;
        const float rotspeed = 0.005f;
        public override void Load()
        {
            var a = w.GetMouseInput();
            a.ResetMousePosition();
            a = w.GetMouseInput();
            lastmousepos = new Vec2(a.PosX, a.PosY);
        }
        public override void Update()
        {
            var ki = w.GetKeyboardInput();
            Vec3 pos = new Vec3();
            pos.Z += ki.KeyDown(Key.W) ? speed : 0;
            pos.Z -= ki.KeyDown(Key.S) ? speed : 0;
            pos.Y += ki.KeyDown(Key.Space) ? speed : 0;
            pos.Y -= ki.KeyDown(Key.ShiftLeft) ? speed : 0;
            pos.X += ki.KeyDown(Key.D) ? speed : 0;
            pos.X -= ki.KeyDown(Key.A) ? speed : 0;
            cam.Move(pos.X, pos.Y, pos.Z);
            var mi = w.GetMouseInput();
            Vec2 rot = new Vec2(mi.PosX, mi.PosY);
            cam.AddRotation((lastmousepos.X - rot.X) * rotspeed, (lastmousepos.Y - rot.Y) * rotspeed);
            mi.ResetMousePosition();
            //lastmousepos = rot;
            t++;
            Console.WriteLine("{0},{1},{2}",cam.Position.X, cam.Position.Y, cam.Position.Z);
        }
    }
}
