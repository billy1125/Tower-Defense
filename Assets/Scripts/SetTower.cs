using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SetTower : MonoBehaviour
{
    public GameObject TowerPrefab; //砲塔預置物件
    public GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //滑鼠點選事件
        {
            //取得滑鼠點選遊戲場景中的位置點
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(pz);
            //再將滑鼠點選遊戲場景中的位置點，轉換成為格線系統上的格子位置
            GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
            Vector3Int cellPosition = gridLayout.WorldToCell(pz);
            //Debug.Log(cellPosition);
            //格線系統上的格子位置取得之後，因為只是格子的原點位置，所以再把x、y軸再加0.5f，就能獲得格子的中心點位置
            Vector3 towerPosition = gridLayout.CellToWorld(cellPosition) + new Vector3(0.5f, 0.5f, 0);
            //獲得格子的中心點位置後，再確認這個格子上有沒有砲塔設置點的圖樣，有的話再把砲塔放上去
            if (GetComponent<Tilemap>().GetTile(cellPosition) != null)
            {
                //Debug.Log("有砲塔");
                if (gameManager.Money >= 10)
                {
                    Instantiate(TowerPrefab, towerPosition, this.transform.rotation);
                    gameManager.UpdateMoney(-10);
                }
            }
        }
    }
}
