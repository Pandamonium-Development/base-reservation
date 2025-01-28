import { isNil } from "lodash"
import { ToolbarOption } from "./ToolbarOption"
import { routesProtected } from "navigation/ProtectedNavigation"

export const MenuOptions = () => {
    return (
        <>
            {routesProtected.filter((route) => !isNil(route.name) && route.showInMenu).map((route) => {
                return <ToolbarOption key={route.name} OptionName={route.name} OptionPath={route.path} />
            })}
        </>
    )
}