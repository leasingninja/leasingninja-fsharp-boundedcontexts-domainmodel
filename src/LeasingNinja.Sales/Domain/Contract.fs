namespace LeasingNinja.Sales.Domain

open NMolecules.DDD

(*
module Say =
    let hello name =
        printfn "Hello %s" name
*)

[<ValueObject>]
type ContractNumber = ContractNumber of string

[<ValueObject>]
type Customer = Customer of string

[<ValueObject>]
type Car = Car of string

[<ValueObject>]
type Currency =
    | EUR
    | GBP
    | USD

[<ValueObject>]
type Amount =
    {
        amountValue: int
        currency: Currency
    }

[<ValueObject>]
type SignDate = SignDate of string //TODOLocalDate


type UnsignedContract =
    {
        number: ContractNumber //public ContractNumber Number => Identity;
        lessee: Customer
        car: Car
        price: Amount
    }
type SignedContract =
    {
        number: ContractNumber
        lessee: Customer
        car: Car
        price: Amount
        
        //public SignDate SignDate { get; private set; }
        signDate: SignDate
    }

[<Entity>]
module Contract =
    type Contract =
           | EmptyContract
           | UnsignedContract of UnsignedContract
           | SignedContract of SignedContract

    type SignContract = UnsignedContract -> SignDate -> SignedContract
    
    //let sign contract signDate: UnsignedContract =
    let signContract : SignContract =
        fun unsignedContract signDate ->
            
            let contractNumber =
                unsignedContract.number
                |> ContractNumber.create
            
            let result = SignedContract with contract
            result