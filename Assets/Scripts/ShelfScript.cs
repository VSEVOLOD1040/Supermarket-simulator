using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShelfScript : MonoBehaviour, IInteractable
{
    public ShelfUI shelfUI;
    public Button SetPriceButton;
    public ProductSO current_product;
    public TextMeshProUGUI PriceTag;
    
    public void Interact(GameObject interactor = null)
    {
        if (interactor != null)
        {
            GameObject item = interactor.GetComponent<PlayerScript>().CurrentItem; // Можливо переробити на більш гнучку логіку

            if (item == null)
            {
                return;
            }

            if (item.TryGetComponent<BoxScript>(out BoxScript box_script))
            {
                foreach (var slot in slots)
                {
                    if (slot.GetComponent<ProductSlot>().Product == null)
                    {
                       
                        if (current_product == null)
                        {
                            current_product = box_script.product;
                            UpdatePriceText();

                        }

                        if (box_script.product == current_product)
                        {
                            slot.GetComponent<ProductSlot>()?.SetProduct(box_script.product);

                            box_script.TakeProduct();

                            break;
                        }

                    }
                }
            }
            if (item.TryGetComponent<PriceToolScript>(out PriceToolScript priceToolScript))
            {
                Debug.Log(current_product);
                if (current_product != null)
                {
                    shelfUI.SwitchPanel(true, current_product);
                    SetPriceButton.onClick.AddListener(SetPrice);
                }
                
            }
            
        }
    }
    public void UpdatePriceText()
    {
        if (current_product != null)
        {
            PriceTag.text = $"{current_product.Price}$";

        }

    }
    public void SetPrice()
    {
        if (current_product != null)
        {
            current_product.Price = shelfUI.GetPrice();
            SetPriceButton.onClick.RemoveListener(SetPrice);
            shelfUI.SwitchPanel(false);
            EventBus.UpdatePriceTags();


        }
    }

    public ProductSO TakeProduct(int Amount)
    {
        Debug.Log("TakeProduct " + Amount);
        ProductSO product = null;
        for (int i = 0; i < Amount; i++)
        {
            foreach (var slot in slots)
            {
                if (slot.GetComponent<ProductSlot>().Product != null)
                {
                    product = slot.GetComponent<ProductSlot>().Product;
                    slot.GetComponent<ProductSlot>().Product = null;
                    Destroy(slot.transform.GetChild(0).gameObject);

                    if (CheckIfShelfEmpty())
                    {
                        current_product = null;
                        PriceTag.text = "";
                    }
                    break;
                }
            }
        }
        if (product) return product;

        return null;

    }

    public int GetProductAmount()
    {
        int count = 0;
        foreach (var slot in slots)
        {
            if (slot.GetComponent<ProductSlot>().Product != null)
            {
                
                count++;
            }
        }
        return count;
    }

    bool CheckIfShelfEmpty()
    {
        foreach (var slot in slots)
        {
            if (slot.GetComponent<ProductSlot>().Product != null)
            {
                return false;
            }
        }
        return true;
    }

    public List<GameObject> slots;
    public GameObject SlotsParent;
    // Start is called before the first frame update
    void Start()
    {
        slots = new List<GameObject>();

        foreach (Transform child in SlotsParent.transform)
        {
            slots.Add(child.gameObject);
        }
    }
    private void Awake()
    {
        EventBus.UpdatePriceTags += UpdatePriceText;
    }
    private void OnDestroy()
    {
        EventBus.UpdatePriceTags -= UpdatePriceText;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
