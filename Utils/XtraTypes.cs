namespace Webshop.Utils.Xtratypes
{
    public enum Gender{MAN, WOMAN, UNSPECIFIED, UNISEX, ALL} //In the file Xtensions there is an explicit conversion from GenderToString, when added
    //a new gender to the enum also add a new gender to the switch statement in the Xtension method.
    public enum Extra{REGULAR, LIMITED, SALE, EXTRAVAGANT, ALL}
    public enum OrderStatus{TO_BE_PAID, TO_BE_PACKED, PACKED, SEND, DELIVERED, ALL}
    public enum StockInicator{OUTOFORDER, LESSTHANFIVE, PLENTY }
    public enum PaymentMethod{PAYPAL, TRANSFER}
}