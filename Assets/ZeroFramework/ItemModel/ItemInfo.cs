using UnityEngine;

namespace ItemSpace
{


    public enum ItemType
    {
        //消耗品，装备类，武器类，其他
        Prop, Equipment, Weapon, Other
    }

    /// <summary>
    /// 物品品质
    /// </summary>
    public enum ItemQuality
    {
        NONE = 0, 一般 = 1, 精良 = 2, 优秀 = 3, 史诗 = 4, 传说 = 5, 神器 = 6
    }


    public enum WeaponType
    {
        斧, 锤, 棒, 剑, 刀, 弓, 弩, 法杖
    }


    /// <summary>
    /// 物品类
    /// </summary>
    [System.Serializable]
    public class Item
    {
        public int Id { get; set; } //ID，同一类型物品一样
        public int ItemId { get; set; } //物品id，全物品唯一
        public string Name { get; set; } //物品名字
        public string Icon { get; set; } //物品图标
        public ItemType ItemType { get; set; } //物品类型
        public float SellPrice { get; set; }//出售价格
        public float BuyPrice { get; set; }//购买价格
        public ItemQuality Quality { get; set; }//品质
        public string Desc { get; set; }//描述信息

        public Item()
        {

        }

        public Item(int _id, int _itemId, string _name, string _icon, ItemType _itemType,
                    float _sellPrice, float _buyPrice, ItemQuality _quality, string _desc)
        {
            this.Id = _id;
            this.ItemId = _itemId;
            this.Name = _name;
            this.Icon = _icon;
            this.ItemType = _itemType;
            this.SellPrice = _sellPrice;
            this.BuyPrice = _buyPrice;
            this.Quality = _quality;
            this.Desc = _desc;
        }

    }
    /// <summary>
    /// 道具
    /// </summary>
    public class Prop : Item
    {
        public float Count { get; set; }//数量
        public float AddHp { get; set; }//回血
        public float AddMp { get; set; }//回血

        public Prop()
        {

        }

        public Prop(int _id, int _itemId, string _name, string _icon, ItemType _itemType,
                    float _sellPrice, float _buyPrice, ItemQuality _quality, string _desc,
                    float _count, float _addHp, float _addMp
                    )
                : base(_id, _itemId, _name, _icon, _itemType,
                _sellPrice, _buyPrice, _quality, _desc)
        {
            this.Count = _count;
            this.AddHp = _addHp;
            this.AddMp = _addMp;
        }
    }

    /// <summary>
    /// 装备
    /// </summary>
    public class Equipment : Item
    {
        public float Atk { get; set; }//攻击力
        public float AttackSpeed { get; set; }//攻击速度
        public float AttackRange { get; set; }//攻击范围
        // public float Repulse { get; set; }//击退
        // public float Mass { get; set; }//重量
        // public float Durabl { get; set; }//耐久

        public Equipment()
        {

        }
        public Equipment(int _id, int _itemId, string _name, string _icon, ItemType _itemType,
                    float _sellPrice, float _buyPrice, ItemQuality _quality, string _desc,
                    float _atk, float _attackSpeed, float _attackRange
                    )
                : base(_id, _itemId, _name, _icon, _itemType,
                _sellPrice, _buyPrice, _quality, _desc)
        {
            this.Atk = _atk;
            this.AttackSpeed = _attackSpeed;
            this.AttackRange = _attackRange;
            // this.Repulse = _repulse;
            // this.Mass = _mass;
            // this.Durabl = _durabl;
        }
    }

    /// <summary>
    /// 武器
    /// </summary>
    public class Weapon : Item
    {
        public float Atk { get; set; }//攻击力
        public WeaponType WType { get; set; }//武器类型
        public float AttackSpeed { get; set; }//攻击速度
        public float AttackRange { get; set; }//攻击范围

        public Weapon()
        {

        }

        public Weapon(int _id, int _itemId, string _name, string _icon, ItemType _itemType,
                    float _sellPrice, float _buyPrice, ItemQuality _quality, string _desc,
                    float _atk, WeaponType _wType, float _attackSpeed, float _attackRange
                    )
                : base(_id, _itemId, _name, _icon, _itemType,
                _sellPrice, _buyPrice, _quality, _desc)
        {
            this.Atk = _atk;
            this.WType = _wType;
            this.AttackSpeed = _attackSpeed;
            this.AttackRange = _attackRange;
        }
    }

    /// <summary>
    /// 杂项、材料
    /// </summary>
    public class Other : Item
    {

        public Other()
        {

        }
        public Other(int _id, int _itemId, string _name, string _icon, ItemType _itemType,
                    float _sellPrice, float _buyPrice, ItemQuality _quality, string _desc
                    ) : base(_id, _itemId, _name, _icon, _itemType,
                _sellPrice, _buyPrice, _quality, _desc)
        { }

    }
}
