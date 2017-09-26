namespace Webshop.Xtratypes{
    public enum Gender{MAN, WOMAN, UNSPECIFIED, UNISEX} //In the file Xtensions there is an explicit conversion from GenderToString, when added
    //a new gender to the enum also add a new gender to the switch statement in the Xtension method.
    public enum Extra{REGULAR, LIMITED, SALE, EXTRAVAGANT}
    public enum OrderStatus{BUSY, SEND, DELAYED}
}