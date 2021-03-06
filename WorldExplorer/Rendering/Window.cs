﻿using System;
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
            Material mat = new Material(new Vec3(0.7, 0.7, 0.7), new Vec3(0, 0, 0), new Vec3(0.9, 0.9, 0.9));
            window = new Window(500, 500, "WorldExplorer");
            window.Wake();
            int texId = window.loadImage("C:/Users/Aleks.Aleks-PC/Desktop/generator/NBT/texpack/assets/minecraft/textures/blocks/planks_oak.png");
            foreach (KeyValuePair<ComparableTuple2<int, int>, RegionFile> region in world.regions)
            {
                GameObject reg = new GameObject("r" + region.Key.Item0 + "." + region.Key.Item1);
                foreach (KeyValuePair<ComparableTuple2<int, int>, Column> column in region.Value.columns)
                {
                    GameObject col = new GameObject("c" + column.Key.Item0 + "." + column.Key.Item1);
                    col.SetParent(reg);
                    LinkedList<Tuple<Vec3[], Vec3[], Vec2[], Vec3>> meshs = new LinkedList<Tuple<Vec3[], Vec3[], Vec2[], Vec3>>();
                    foreach (KeyValuePair<ComparableTuple3<byte, byte, byte>, Block> block in column.Value.blocks)
                    {
                        Block b = block.Value;
                        int x, y, z;
                        x = b.x + column.Value.xPos * 16;
                        z = b.z + column.Value.zPos * 16;
                        y = b.y;
                        Block _t;
                        Tuple<Vec3[], Vec3[], Vec2[], Vec3> m = CreateCustomMesh(
                            !column.Value.blocks.TryGetValue(b.x, (byte)(b.y + 1), b.z, out _t),
                            !column.Value.blocks.TryGetValue(b.x, (byte)(b.y - 1), b.z, out _t),
                            !column.Value.blocks.TryGetValue(b.x, b.y, (byte)(b.z - 1), out _t),
                            !column.Value.blocks.TryGetValue(b.x, b.y, (byte)(b.z + 1), out _t),
                            !column.Value.blocks.TryGetValue((byte)(b.x - 1), b.y, b.z, out _t),
                            !column.Value.blocks.TryGetValue((byte)(b.x + 1), b.y, b.z, out _t), new Vec3(x, y, z));
                        if (m.Item2.Length == 0) continue;
                        meshs.AddFirst(m);
                    }
                    MeshFilter mF = col.AddComponent<MeshFilter>();
                    mF.mesh = combineMeshes(meshs);//cleanMesh(combineMeshes(meshs)); the ctor kinda already cleans the mesh
                    mF.material = mat;
                    mF.material.TextureID = texId;
                    col.AddComponent<MeshRenderer>();
                    //col.Active = new Random().NextDouble()>0.5;
                    foreach (GameObject go in col.GetChildren())
                    {
                        go.Delete();
                    }
                    Console.WriteLine(column.Key.Item0 + "." + column.Key.Item1);
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
        private Tuple<Vec3[], Vec3[], Vec2[], Vec3> CreateCustomMesh(bool up, bool down, bool left, bool right, bool front, bool back, Vec3 pos)
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
            return new Tuple<Vec3[],Vec3[],Vec2[],Vec3>(verts,tris.ToArray(),textures,pos);
        }

        private Mesh combineMeshes(LinkedList<Tuple<Vec3[],Vec3[],Vec2[],Vec3>> meshes)
        {
            List<Vec3> verts = new List<Vec3>();
            List<Vec3> colors = new List<Vec3>();
            List<Vec2> texs = new List<Vec2>();
            List<Vec3> tris = new List<Vec3>();
            int offset = 0;
            var iter = meshes.GetEnumerator();
            while (iter.MoveNext()) {
                var go = iter.Current;
                verts.AddRange(go.Item1.Select(r => r + go.Item4));
                colors.AddRange(Enumerable.Repeat(new Vec3(), go.Item1.Length));
                texs.AddRange(go.Item3);
                tris.AddRange(go.Item2.Select(r => r += new Vec3(offset,offset,offset)));
                offset += go.Item1.Length;
            }
            return new Mesh(verts, tris, colors, texs);
        }

        private Mesh cleanMesh(Mesh m)
        {
            int vertL = m.faces.Count;
            Vec3[] tris = m.Triangles;
            List<Vec3> verts = m.Verticies.ToList();
            List<Vec2> texs = m.Textures.ToList();
            for (int i = 0; i < vertL; i++)
            {
                if (!triAppear(i, tris))
                {
                    verts.RemoveAt(i);
                    texs.RemoveAt(i);
                    for(int j = 0; j < tris.Length; ++j)
                    {
                        if (tris[j].X > i) tris[j].X--;
                        if (tris[j].Y > i) tris[j].Y--;
                        if (tris[j].Z > i) tris[j].Z--;
                    }
                }
            }
            return new Mesh(verts, tris, Enumerable.Repeat(new Vec3(), verts.Count), texs);
        }

        private bool triAppear(int i, Vec3[] tris)
        {
            foreach(Vec3 tri in tris)
            {
                if (tri.X == i || tri.Y == i || tri.Z == i) 
                {
                    return true;
                }
            }
            return false;
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
            window.Title = window.FrameRate.ToString();
        }
    }
}
