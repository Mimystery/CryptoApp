import { useState, useContext } from "react"
import { Flex, Select, Space, Typography } from "antd"
import { CryptoContext } from '../../context/crypto-context';

export default function AddCoinForm(){
const [coin, setCoin] = useState(null)
const {selectCoins} = useContext(CryptoContext)

const handleSelect = (value) =>{
  console.log(value)
}

if(!coin){
    return(
        <Select
    style={{ width: '100%' }}
    onSelect={(v) => setCoin(selectCoins.find((c) => c.symbol === v))}
    value="Select coin"
    options={selectCoins.map((coin)=>({
      label: coin.name,
      value: coin.symbol,
      icon: coin.imageUrl
    }))}
    optionRender={option => (
      <Space>
        <img src={option.data.icon} alt={option.data.label} style={{width: 20}}/> {option.data.label}
      </Space>
    )}
  />
    )
}

    return(
        <form>
            <Flex align="center">
                <img src={coin.imageUrl} alt={coin.name} style={{width: 40, marginRight: 10}}/>
                <Typography.Title level={2} style={{margin: 0}}>
                    {coin.name}
                </Typography.Title>
            </Flex>
        </form>
    )
}