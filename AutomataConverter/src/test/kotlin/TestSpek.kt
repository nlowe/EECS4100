package AutomataConverter.Tests

import org.amshove.kluent.*
import org.jetbrains.spek.api.Spek
import org.jetbrains.spek.api.dsl.describe
import org.jetbrains.spek.api.dsl.it
import org.jetbrains.spek.api.dsl.on

object TestSpek: Spek({
    describe("A sample test") {
        on("running the test") {
            it("passes") {
                1 + 1 shouldBe 2
            }
        }
    }
})