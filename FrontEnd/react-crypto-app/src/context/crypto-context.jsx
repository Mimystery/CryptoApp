import { createContext } from "react";
import { useState, useEffect } from "react";
import { fetchCryptoWallet, fetchPrice, fetchSelectCoins } from '../api';
import { percentDifference } from '../utils'

export const CryptoContext = createContext({
    wallet: [],
    crypto: [],
    selectCoins: [],
    isAuthenticated: false,
    user: null,
    loading: false,
})

export const CryptoContextProvider = ({children}) => {
const [prices, setPrices] = useState([]);
const [wallet, setWallet] = useState([]);
const [loading, setLoading] = useState(true);
const [selectCoins, setSelectCoins] = useState([]);
const [isAuthenticated, setIsAuthenticated] = useState(false);
const [user, setUser] = useState(null);

useEffect(() =>{
  const token = localStorage.getItem('jwt');
  const savedUser = localStorage.getItem('userData')
  if(token){
    setIsAuthenticated(true);
    setUser(JSON.parse(savedUser))
    console.log(isAuthenticated)
    console.log(user)
  }
  else{
    setIsAuthenticated(false);
  }
}, [])

  useEffect(() => {
    
    const fetchCryptoPrice = async () =>{
        let prices = await fetchPrice();
        let wallet = await fetchCryptoWallet();

        setWallet(wallet.map(walletCoin => {
          const coin = prices.find((c) => c.symbol === walletCoin.symbol)
          return {
            grow: walletCoin.price < coin.price,
            growPercent: percentDifference(walletCoin.price, coin.price),
            totalMoney: walletCoin.amount * coin.price,
            totalProfit: walletCoin.amount * coin.price - walletCoin.amount * walletCoin.price,
            ...walletCoin
          }
        }))
        setPrices(prices)
        setLoading(false)
    }

    fetchCryptoPrice()
    const interval = setInterval(fetchCryptoPrice, 2000); 

    return () => clearInterval(interval); 
  }, []);

  useEffect(() => {

    const fetchCryptoSelect = async () => {
        setLoading(true)
        let selectCoins = await fetchSelectCoins();
        setSelectCoins(selectCoins)
        setLoading(false)

    }
    fetchCryptoSelect()
  }, []);


    return(
    <CryptoContext.Provider value={{prices, wallet, loading, selectCoins, isAuthenticated, 
    setIsAuthenticated, user, setUser}}>
        {children}
    </CryptoContext.Provider>
    )
}