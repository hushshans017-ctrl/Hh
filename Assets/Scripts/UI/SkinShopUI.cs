using UnityEngine;
using UnityEngine.UI;

public class SkinShopUI : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup skinGridLayout;
    [SerializeField] private GameObject skinItemPrefab;
    [SerializeField] private Text currencyText;
    [SerializeField] private Text skinNameText;
    [SerializeField] private Text skinDescriptionText;
    [SerializeField] private Image skinPreviewImage;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button selectButton;

    private CharacterSkinSystem skinSystem;
    private int selectedSkinIndex = 0;

    private void Start()
    {
        skinSystem = CharacterSkinSystem.Instance;
        PopulateSkinShop();
        UpdateCurrencyDisplay();
    }

    private void PopulateSkinShop()
    {
        var skins = skinSystem.GetAllSkins();

        foreach (int i in new int[skins.Count])
        {
            CreateSkinItem(i, skins[i]);
        }
    }

    private void CreateSkinItem(int index, CharacterSkinSystem.CharacterSkin skin)
    {
        GameObject skinItem = Instantiate(skinItemPrefab, skinGridLayout.transform);
        Button itemButton = skinItem.GetComponent<Button>();

        if (itemButton != null)
        {
            itemButton.onClick.AddListener(() => SelectSkinForPreview(index));
        }

        // Set visual indicators for rarity
        Image itemImage = skinItem.GetComponentInChildren<Image>();
        if (itemImage != null)
        {
            itemImage.sprite = skin.thumbnailSprite;
            
            // Color code by rarity
            switch (skin.rarity)
            {
                case "Common":
                    itemImage.color = Color.gray;
                    break;
                case "Rare":
                    itemImage.color = new Color(0.2f, 0.6f, 1.0f); // Blue
                    break;
                case "Epic":
                    itemImage.color = new Color(0.8f, 0.2f, 1.0f); // Purple
                    break;
                case "Legendary":
                    itemImage.color = new Color(1.0f, 0.8f, 0.0f); // Gold
                    break;
            }
        }

        // Lock indicator for unlocked skins
        if (!skin.isUnlocked)
        {
            Text lockText = skinItem.GetComponentInChildren<Text>();
            if (lockText != null)
                lockText.text = "🔒";
        }
    }

    private void SelectSkinForPreview(int index)
    {
        selectedSkinIndex = index;
        var skin = skinSystem.GetSkin(index);

        if (skin != null)
        {
            skinNameText.text = skin.skinName + " (" + skin.rarity + ")";
            skinDescriptionText.text = skin.description;
            
            if (skinPreviewImage != null && skin.thumbnailSprite != null)
                skinPreviewImage.sprite = skin.thumbnailSprite;

            // Update button states
            if (skin.isUnlocked)
            {
                selectButton.interactable = true;
                buyButton.gameObject.SetActive(false);
            }
            else
            {
                selectButton.interactable = false;
                buyButton.gameObject.SetActive(true);
                buyButton.GetComponentInChildren<Text>().text = "Buy - " + skin.cost + "💰";
            }
        }
    }

    public void BuySkin()
    {
        if (skinSystem.UnlockSkin(selectedSkinIndex))
        {
            UpdateCurrencyDisplay();
            SelectSkinForPreview(selectedSkinIndex);
        }
    }

    public void EquipSkin()
    {
        skinSystem.SelectSkin(selectedSkinIndex);
        Debug.Log("Equipped skin: " + skinSystem.GetSkin(selectedSkinIndex).skinName);
    }

    private void UpdateCurrencyDisplay()
    {
        currencyText.text = "💰 " + skinSystem.GetPlayerCurrency();
    }
}
