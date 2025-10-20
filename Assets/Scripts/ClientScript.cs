using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class ClientScript : MonoBehaviour
{
    public MarketDataSO MarketData;
    public int MaxProductAmountForOneProductType;
    public Dictionary<string, int> ProductList;
    public Transform CashPoint; // “имчасово
    public Transform ExitPoint; // не“имчасово

    public List<Vector3> MovementList = new List<Vector3>();
    public List<ShelfScript> ShelfList = new List<ShelfScript>();

    public float Distance;

    public int CurrentTargetIndex = 0;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ExitPoint = GameObject.Find("ExitPoint").transform;
        CashPoint = GameObject.Find("CashPoint").transform.GetChild(1);

        ProductList = GetRandomProducts(MarketData.GetProducts());
        //ProductList = GetTestProductList();
        PrintDictionary(ProductList);

        List<Vector3> locations = FindProductLocation();

        if (locations != null && locations.Count > 0)
        {
            MovementList.AddRange(locations);
        }

        if (MovementList.Count > 0)
        {
            MovementList.Add(CashPoint.position);

        }
        MovementList.Add(ExitPoint.position);

        agent.SetDestination(MovementList[CurrentTargetIndex]);
    }

    // Update is called once per frame
    void Update()
    {

        Distance = agent.remainingDistance;



        HandleMovement();

        

    }
    
    IEnumerator SetDestination(int Index)
    {
        yield return new WaitForSeconds(Random.Range(2,5));
        agent.SetDestination(MovementList[Index]);
    }


    void AddToProductList(ProductSO product)
    {

    }
    void HandleMovement()
    {
           

        if (agent.destination != null && agent.remainingDistance < 0.001f && CurrentTargetIndex < MovementList.Count)
        {

            // “ут потр≥бно додати перев≥рку, щоб кл≥Їнт не м≥г вз€ти б≥льше товару, н≥ж потр≥бно 
            if (CurrentTargetIndex >= 0 && CurrentTargetIndex < ShelfList.Count)
            {
                Debug.Log("Current index " + CurrentTargetIndex);

                string ProductName = ShelfList[CurrentTargetIndex].current_product.Name;
                int AmountNeeded = ProductList[ProductName];
                ProductSO product = ShelfList[CurrentTargetIndex].TakeProduct(AmountNeeded);
                Debug.Log("Product taken " + product.Name);

                //ShelfList.RemoveAt(CurrentTargetIndex);
            }

            CurrentTargetIndex++;
            //if (CurrentTargetIndex < MovementList.Count)
            //{
              
            agent.destination = MovementList[CurrentTargetIndex];
            //}
        }
    }
    void PrintDictionary(Dictionary<string, int> dict)
    {
        foreach (KeyValuePair<string, int> pair in dict)
        {
            Debug.Log($"{pair.Key} : {pair.Value}");
        }
    }

    public Dictionary<string, int> GetRandomProducts(List<ProductSO> products)
    {
        Dictionary<string, int> result = new Dictionary<string, int>();

        if (products == null || products.Count == 0)
            return result;

        int countToSelect = Random.Range(2, products.Count + 1);
        List<ProductSO> tempList = new List<ProductSO>(products);
        Shuffle(tempList);

        for (int i = 0; i < countToSelect; i++)
        {
            ProductSO product = tempList[i];
            string productName = product.Name;

            int quantity = GetRandomAmount();

            if (!result.ContainsKey(productName))
                result.Add(productName, quantity);
        }

        return result;
    }

    public Dictionary<string, int> GetTestProductList()
    {
        Dictionary<string, int> result = new Dictionary<string, int>();

        result.Add("Bread", GetRandomAmount());
        result.Add("Milk", GetRandomAmount());

        return result;
    }

    private int GetRandomAmount()
    {
        int quantity = 1;
        float chance = 1f;

        while (quantity < MaxProductAmountForOneProductType)
        {
            chance *= 0.5f;
            if (Random.value < chance)
                quantity++;
            else
                break;
        }

        return quantity;
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }


    //public List<Vector3> FindProductLocation()
    //{
    //    //     ProductSO product = MarketData.GetProducts().Find(p => p.name == productName);
    //    //
    //    List<Vector3> locations = new List<Vector3>();

    //    ShelfScript[] allObjects = FindObjectsOfType<ShelfScript>();

    //    foreach (string ProductName in ProductList.Keys)
    //    {

    //        ProductSO targetProduct = MarketData.GetProducts().Find(p => p.Name == ProductName);
    //        foreach (ShelfScript shelfScript in allObjects)
    //        {
    //            int Amount = ProductList[ProductName];
    //            if (shelfScript.current_product == targetProduct)
    //            {
    //                Transform npcPoint = shelfScript.transform.parent.Find("NPCPoint");
    //                if (npcPoint != null)
    //                {
    //                    if (shelfScript.GetProductAmount() >= ProductList[ProductName])
    //                    {
    //                        ShelfList.Add(shelfScript);
    //                        locations.Add(npcPoint.position);
    //                    }
    //                    else
    //                    {
    //                        List<ShelfScript> tempShelfList = new List<ShelfScript>();
    //                        foreach (ShelfScript shelfScript2 in allObjects)
    //                        {

    //                            if (shelfScript2.current_product == targetProduct)
    //                            {
    //                                tempShelfList.Add(shelfScript2);
    //                            }
    //                        }


    //                        foreach (ShelfScript tempShelf in tempShelfList)
    //                        {
    //                            Amount -= tempShelf.GetProductAmount();

    //                            if (Amount > 0)
    //                            {
    //                                ShelfList.Add(tempShelf);
    //                                locations.Add(tempShelf.transform.parent.Find("NPCPoint").position);
    //                            }
    //                            else
    //                            {

    //                                ShelfList.Add(tempShelf);
    //                                locations.Add(tempShelf.transform.parent.Find("NPCPoint").position);
    //                                break;
    //                            }
    //                        }

    //                    }
    //                }

    //            }
    //        }


    //    }

    //    if (locations.Count > 0) return locations;

    //    return null;
    //}

    public List<Vector3> FindProductLocation()
    {
        //     ProductSO product = MarketData.GetProducts().Find(p => p.name == productName);
        //
        List<Vector3> locations = new List<Vector3>();

        ShelfScript[] allObjects = FindObjectsOfType<ShelfScript>();

        Dictionary<ProductSO, int> targetProducts = new Dictionary<ProductSO, int>();
        foreach (string ProductName in ProductList.Keys)
        {
            ProductSO targetProduct = MarketData.GetProducts().Find(p => p.Name == ProductName);

            targetProducts.Add(targetProduct, ProductList[ProductName]);

        }  

        foreach (var Product in targetProducts)

        {
            int Amount = Product.Value;

            foreach (ShelfScript shelfScript in allObjects)
            {
                Debug.Log("Checking shelf for product " + Product.Key.Name);

                if (shelfScript.current_product == Product.Key)
                {
                    Transform npcPoint = shelfScript.transform.parent.Find("NPCPoint");
                    if (npcPoint != null)
                    {
                        if (shelfScript.GetProductAmount() >= Product.Value)
                        {
                            ShelfList.Add(shelfScript);
                            locations.Add(npcPoint.position);
                        }
                        else
                        {
                            List<ShelfScript> TargetProductShelfList = new List<ShelfScript>();
                            foreach (ShelfScript shelfScript2 in allObjects)
                            {

                                if (shelfScript2.current_product == Product.Key)
                                {
                                    TargetProductShelfList.Add(shelfScript2);
                                }
                            }


                            foreach (ShelfScript tempShelf in TargetProductShelfList)
                            {
                                Amount -= tempShelf.GetProductAmount();

                                if (Amount > 0)
                                {
                                    ShelfList.Add(tempShelf);
                                    locations.Add(tempShelf.transform.parent.Find("NPCPoint").position);
                                }
                                else
                                {

                                    ShelfList.Add(tempShelf);
                                    locations.Add(tempShelf.transform.parent.Find("NPCPoint").position);
                                    break;

                                }

                            }
                        }
                    }


                }
                break;

            }
        }

       


        

        if (locations.Count > 0) return locations;

        return null;
    }
}
    
