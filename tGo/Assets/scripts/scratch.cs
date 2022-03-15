    // public int checkLiberties()
    // {
    //     this.libCount = 0;
    //     string sameTeam = this.color;

    //     foreach (var stone in this.sides)
    //     {
    //         if (stone == null)
    //         {
    //         this.libs[this.libCount] = 1;
    //         this.libCount++;
    //         }
    //         else if (stone.color == sameTeam)
    //         {
    //             this.libs[this.libCount] = 0;
    //             this.libCount++;
    //              //add both to group
    //         }
    //     }
    //     this.liberties = this.libs.Sum();
    //     return this.liberties;
    // }

    // public void updateLiberties()
    // {
    //     foreach (var s in this.sides)
    //         if (s != null)
    //             s.checkLiberties();
    // }


   // private bool checkGroup(Stone stone)
    // {
    //     string sameTeam = stone.color;

    //     //is this playing position putting stone in existing group?
    //     foreach (Stone side in stone.sides)
    //         if (side != null)
    //             if (side.color == sameTeam)
    //             {
    //                 addToGroup(stone, side.go.GetComponent<Group>());
    //                 return true;
    //             }
    //     return false;
    // }

    // private int checkGroupLiberties(Group group)
    // {
    //     foreach (var stone in group.stonesInGroup)
    //     {
    //         checkStoneLiberties(stone);
    //         //group.liberties = (stone.ll+stone.rl+stone.ul+stone.dl);
    //     }
    //     return group.liberties;
    // }

//     private void addNewGroup(Stone stone)
//     {
//             int stonecounter = 0;

//             stone.go.AddComponent<Group>();
//             Group group = stone.go.GetComponent<Group>();

//             group.color = stone.color;
//             group.ID = groupCounter;
//             groupCounter++;

//             group.stonesInGroup = new Stone[10];
//             group.stonesInGroup[stonecounter] = stone;
//             group.stoneCount++;
//     }

//     private Group addToGroup(Stone stone, Group group)
//     {
//         group.stonesInGroup[group.stoneCount] = stone;
//         return group;
//     }

// }

//private void checkStoneLiberties(Stone stone)
    // {
    //     if (stone != null)
    //     {
    //         foreach (Stone ston in stone.sides)
    //         {
    //             string sameTeam = stone.color;

    //             if(ston == null)
    //             {
    //                 if (ston == stone.leftStone)
    //                     stone.ll = 1;
    //                 if (ston == stone.rightStone)
    //                     stone.rl = 1;
    //                 if (ston == stone.upStone)
    //                     stone.ul = 1;
    //                 if (ston == stone.downStone)
    //                     stone.dl = 1;
    //             }
    //             else if (ston.color == sameTeam)
    //             {
    //                 addToGroup(ston, stone.GetComponent<Group>());
    //                 if (ston == stone.leftStone)
    //                     stone.ll = 0;
    //                 if (ston == stone.rightStone)
    //                     stone.rl = 0;
    //                 if (ston == stone.upStone)
    //                     stone.ul = 0;
    //                 if (ston == stone.downStone)
    //                     stone.dl = 0;
    //             }
    //             else
    //             {
    //                 if (ston == stone.leftStone)
    //                     stone.ll = 0;
    //                 if (ston == stone.rightStone)
    //                     stone.rl = 0;
    //                 if (ston == stone.upStone)
    //                     stone.ul = 0;
    //                 if (ston == stone.downStone)
    //                     stone.dl = 0;
    //             }
    //             stone.liberties = (stone.ll+stone.rl+stone.ul+stone.dl);
    //         }
    //     }
    // }



//     {
//         int groupcount = 0;
//         int count = 0;
//         string sameTeam = stone.color;

//         stone.ll=0;
//         stone.rl=0;
//         stone.ul=0;
//         stone.dl=0;



//         // Stone leftstone, rightstone, upstone, downstone;
        
//         // leftstone = stones[stone.left.x,stone.left.y];
//         // rightstone = stones[stone.right.x,stone.right.y];
//         // upstone = stones[stone.up.x,stone.up.y];
//         // downstone = stones[stone.down.x,stone.down.y];

//         if (stones[stone.left.x,stone.left.y] == null)
//             stone.ll = 1;

//         else if (stones[stone.left.x,stone.left.y].color == sameTeam)
//         {
//             //new group
//             GameObject groEmpty = new GameObject(string.Format("group {0}",groupcount ));
//             groupcount++;
//             //assign and get component group script
//             groEmpty.AddComponent<Group>();
//             Group groComp = groEmpty.GetComponent<Group>();
//             //add both stones to group;
//             groComp.stonesInGroup = new Stone[25];
//             groComp.stonesInGroup[count] = stone;
//             count++;
//             groComp.stonesInGroup[count] = stones[stone.left.x,stone.left.y];

//         }

//         else if (stones[stone.left.x,stone.left.y].color != sameTeam)
//             stone.ll = 0;

//         stone.liberties = (stone.ll);

//     }
//     private void checkGroupLiberties(Group group)
//     {
//         foreach (var ston in group.stonesInGroup)
//             Debug.Log(ston);
//     }

    
// }




        // stone.left = new Vector2Int(stone.x-1,stone.y);
        // stone.right = new Vector2Int(stone.x+1,stone.y);
        // stone.up = new Vector2Int(stone.x,stone.y+1);
        // stone.down = new Vector2Int(stone.x,stone.y-1);

// private int getLiberties(Stone stone)
//     {
//         for (int l=0;l<stone.spaces.Length;l++)
//         {
//             if (stones[stone.spaces[l].x,stone.spaces[l].y] == null)
//                 stone.liberties++;
//         }
//         return stone.liberties;
//     }

//     private void assignLiberties(Stone stone)
//     {
//         int counter = 0;

//         stone.spaces[counter] = stone.left;
//         counter++;
//         stone.spaces[counter] = stone.right;
//         counter++;
//         stone.spaces[counter] = stone.up;
//         counter++;
//         stone.spaces[counter] = stone.down;
//         counter++;
//     }

        //point.layer = LayerMask.NameToLayer("point");
        //point.tag="empty";

        //GameObject stone = new GameObject(string.Format("S X:{0},Y:{1}",xindex,yindex));
    
            // Vector3 temp = new Vector3(xindex,0,yindex);
            // points[xindex,yindex].tag="black";
            // GameObject bs = Instantiate(black_stone);
            // bs.transform.position += temp;
            // stones[counter] = bs;
            // counter++;

    

    //     private GameObject generateOnePoint(int x, int y)
    // {
    //  GameObject point = new GameObject(string.Format("point#({0},{1})",x,y));
    // //  point.transform.parent = transform;

    // //  Mesh mesh = new Mesh();
    // //  point.AddComponent<MeshFilter>().mesh= mesh;
    // //  point.AddComponent<MeshRenderer>();

    //  Vector3 temp = new Vector3(x,0,y);
    //  point.transform.position += temp;

    // // point.layer = LayerMask.NameToLayer("point");
    // point.AddComponent<BoxCollider>();
    // //mesh.RecalculateNormals();
    // // point.tag="empty";
    // return point;
    // }

        // var connections = new List<Stone>();

        // connections.Add(leftstone);
        // connections.Add(rightstone);
        // connections.Add(upstone);
        // connections.Add(downstone);

        // foreach (var connection in connections)
        // {
        //     if (connection == null)
        //         stone.liberties++;
        // }

        // if (stone.liberties == 0)
        //     Destroy(stone.go);
        // {
        //     stone.ll = 0;
        //     GameObject stonegroup = new GameObject("group{groupcount}");
        //     groupcount++;
        //     stonegroup.AddComponent<Group>();
        //     Group g = stonegroup.GetComponent<Group>();
        //     g.stonesInGroup = new Stone[10];
        //     g.stonesInGroup[count] = stone;
        //     count++;
        //     g.stonesInGroup[count] = stones[stone.left.x,stone.left.y];

                // else if (stones[stone.right.x,stone.right.y] == null)
        //     stone.rl =1;
        // else if (stones[stone.up.x,stone.up.y] == null)
        //     stone.ul = 1;
        // else if (stones[stone.down.x,stone.down.y] == null)
        //     stone.dl =1;



                // Stone leftstone, rightstone, upstone, downstone;

        // leftstone = stones[stone.left.x,stone.left.y];
        // rightstone = stones[stone.right.x,stone.right.y];
        // upstone = stones[stone.up.x,stone.up.y];
        // downstone = stones[stone.down.x,stone.down.y];

        // if (leftstone != null)
        //     checkStoneLiberties(leftstone);
        // if (rightstone != null)
        //     checkStoneLiberties(rightstone);
        // if (upstone != null)
        //     checkStoneLiberties(upstone);
        // if (downstone != null)
        //     checkStoneLiberties(downstone);

//int lone, ltwo, lthree, lfour;
    //     foreach (var ston in stone.sides)
    //     {
    //         if(ston == null)
    //         {

    //         }
    //         else if (ston.team == sameTeam)
    //         {

    //         }
    //         else
    //         {

    //         }
    //     }
    // }



          //checkGroupLiberties(stone.go.GetComponent<Group>());
        // if (stone.rightStone != null)
        //     checkStoneLiberties(stone.rightStone);
        // if (stone.leftStone != null)
        //     checkStoneLiberties(stone.leftStone);
        // if (stone.upStone != null)
        //     checkStoneLiberties(stone.upStone);
        // if (stone.downStone != null)
        //     checkStoneLiberties(stone.downStone);
        // if (stone.rightStone.go.GetComponent<Group>() != null)
        //     checkGroupLiberties(stone.rightStone.go.GetComponent<Group>());
        // if (stone.leftStone.go.GetComponent<Group>() != null)
        //     checkGroupLiberties(stone.leftStone.go.GetComponent<Group>());
        // if (stone.upStone.go.GetComponent<Group>() != null)
        //     checkGroupLiberties(stone.upStone.go.GetComponent<Group>());
        // if (stone.downStone.go.GetComponent<Group>() != null)
        //     checkGroupLiberties(stone.downStone.go.GetComponent<Group>());
