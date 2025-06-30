import { Flex, Typography } from "antd"

export default function CoinLabel({coin}){
    return(
    <Flex align="center">
        <img src={coin.imageUrl} alt={coin.name} style={{width: 40, marginRight: 10}}/>
            <Typography.Title level={2} style={{margin: 0}}>
                {coin.name}
            </Typography.Title>
    </Flex>
    )
}