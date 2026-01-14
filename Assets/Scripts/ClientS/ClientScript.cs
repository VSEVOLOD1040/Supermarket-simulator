using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class ClientScript : MonoBehaviour
{
    public MarketDataSO MarketData;
    public int MaxProductAmountForOneProductType;
    public Dictionary<string, int> ProductList;
    public Transform CashPoints; // “имчасово
    public Transform ExitPoint; // Ќе‘актўоне“имчасово

    public List<Vector3> MovementList = new List<Vector3>();
    public List<ShelfScript> ShelfList = new List<ShelfScript>();


    public Dictionary<ProductSO, int> ProductsTaken = new Dictionary<ProductSO, int>();

    public float Distance;

    public int CurrentTargetIndex = 0;
    public NavMeshAgent agent;

    // Start is called before the first frame update

    public bool WillClientBuy(float marketPrice, float currentPrice)
    {
        float chance;

        if (currentPrice <= marketPrice)
        {
            chance = 100f;
        }
        else if (currentPrice >= marketPrice * 3f)
        {
            chance = 0f;
        }
        else if (currentPrice >= marketPrice * 2f)
        {
            chance = 90f;
        }
        else
        {
            float t = (currentPrice - marketPrice) / marketPrice;
            chance = Mathf.Lerp(100f, 90f, t);
        }

        float rnd= Random.Range(0f, 100f);
        return rnd<= chance;
    }
    void AddCashPoint()
    {
        List<GameObject> activeObjects = new List<GameObject>();

        foreach (NavMeshModifier child in CashPoints.transform.GetComponentsInChildren<NavMeshModifier>())
        {
            if (child.gameObject.activeSelf)
            {   
                activeObjects.Add(child.gameObject);
            }
        }

        Debug.Log("Active cash point: " + activeObjects.Count);

        GameObject cash = activeObjects[Random.Range(0, activeObjects.Count)];

        MovementList.Add(cash.transform.GetChild(1).transform.position);
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ExitPoint = GameObject.Find("ExitPoint").transform;
        CashPoints = GameObject.Find("CASH_POINTS").transform;

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

            AddCashPoint();
        }
        else
        {
            Statistic.instance.CustomersDeserved += 1;
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
           

        if (agent.destination != null && agent.remainingDistance < 0.16f && CurrentTargetIndex < MovementList.Count)
        {

            // “ут потр≥бно додати перев≥рку, щоб кл≥Їнт не м≥г вз€ти б≥льше товару, н≥ж потр≥бно 
            if (CurrentTargetIndex >= 0 && CurrentTargetIndex < ShelfList.Count)
            {
                if (ShelfList[CurrentTargetIndex].CheckIfShelfEmpty() == false)
                {
                    Debug.Log("Current index " + CurrentTargetIndex);

                    string ProductName = ShelfList[CurrentTargetIndex].current_product.Name;
                    int AmountNeeded = ProductList[ProductName]; //тут була помилка
                    int AmountTaken = 0;

                    if (WillClientBuy(MarketData.GetPrice(ShelfList[CurrentTargetIndex].current_product)/ MarketData.GetBatchSize(ShelfList[CurrentTargetIndex].current_product), ShelfList[CurrentTargetIndex].current_product.Price))
                    {
                        print("Client decided to buy " + ProductName);
                        ProductSO product = ShelfList[CurrentTargetIndex].TakeProduct(AmountNeeded, out AmountTaken);

                        if (product != null)
                        {

                            if (ProductsTaken.ContainsKey(product))
                            {
                                ProductsTaken[product] += AmountTaken;
                            }
                            else ProductsTaken.Add(product, AmountTaken);

                            if (AmountNeeded - AmountTaken <= 0)
                            {
                                ProductList.Remove(ProductName);
                            }
                            else
                            {
                                ProductList[ProductName] = AmountNeeded - AmountTaken;
                            }
                        }
                    }
                    
                }
                else
                {
                    MovementList.Clear();
                    ShelfList.Clear();
                    CurrentTargetIndex = 0;

                    MovementList = new List<Vector3>();
                    
                    List<Vector3> newLocations = FindProductLocation();
                    if (newLocations != null)
                    {
                        MovementList.AddRange(newLocations);

                    }


                    AddCashPoint();


                    MovementList.Add(ExitPoint.position);

                    agent.SetDestination(MovementList[CurrentTargetIndex]);

                    Debug.Log("Updated movement list. Amount: "+MovementList.Count);
                    return;
                }




                //ShelfList.RemoveAt(CurrentTargetIndex);
            }
            else if(CurrentTargetIndex == MovementList.Count - 2)
            {
                GameObject.Find("Player").GetComponent<PlayerScript>().AddMoney(Pay());
                Statistic.instance.CustomersServed += 1;
            }
            else if (CurrentTargetIndex == MovementList.Count - 1)
            {
                Destroy(gameObject);
            }

            CurrentTargetIndex++;
            if (CurrentTargetIndex < MovementList.Count)
            {

                agent.destination = MovementList[CurrentTargetIndex];
            }
        }
    }

    

    float Pay()
    {
        float total = 0f;
        foreach (var item in ProductsTaken)
        {
            total += item.Key.Price * item.Value;
        }
        if (total > 0)
        {
            print("Client paid: " + total);
            gameObject.GetComponent<AudioSource>().Play();

        }

        return total;
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
        List<Vector3> locations = new List<Vector3>();
        ShelfScript[] allShelves = FindObjectsOfType<ShelfScript>();

        Dictionary<ProductSO, int> targetProducts = new Dictionary<ProductSO, int>();
        foreach (string productName in ProductList.Keys)
        {
            ProductSO targetProduct = MarketData.GetProducts().Find(p => p.Name == productName);
            if (targetProduct != null)
                targetProducts.Add(targetProduct, ProductList[productName]);
        }

        foreach (var productEntry in targetProducts)
        {
            ProductSO product = productEntry.Key; // ѕродукт котрий потр≥бен
            int amountNeeded = productEntry.Value; //  ≥льк≥сть €ка потр≥бна
            List<ShelfScript> productShelves = new List<ShelfScript>();

            foreach (var productShelf in allShelves)
            {
                if (productShelf.current_product == product)
                {
                    productShelves.Add(productShelf);
                }
            }

            productShelves.OrderByDescending(s => s.GetProductAmount());

            foreach (ShelfScript shelf in productShelves)
            {
                int shelfAmount = shelf.GetProductAmount();

                if (shelfAmount <= 0)
                    continue;

                ShelfList.Add(shelf);
                Transform npcPoint = shelf.transform.parent.Find("NPCPoint");
                if (npcPoint != null)
                    locations.Add(npcPoint.position);

                amountNeeded -= shelfAmount;
                if (amountNeeded <= 0)
                    break;
            }
        }

        return locations.Count > 0 ? locations : null;
    }

       
}
    
